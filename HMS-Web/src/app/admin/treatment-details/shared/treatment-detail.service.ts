import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../../../shared/constants/constants';
import { Observable } from 'rxjs';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { TreatmentDetailModel } from './treatment-detail.model';

@Injectable()
export class TreatmentDetailService {
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        const token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    treatmentDetailUrl: string = this.baseUrl + Constants.API_TREATMENT_DETAIL.treatmentDetail;

    public get(params: any): Observable<PaginatedListModel<TreatmentDetailModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<TreatmentDetailModel>>(this.treatmentDetailUrl, { headers: this.headers, params: params });
    }

    public getByMedicalRecordId(params: any): Observable<PaginatedListModel<TreatmentDetailModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<TreatmentDetailModel>>(`${this.treatmentDetailUrl}/GetByMedicalRecordId/${params.medicalRecordId}`, { headers: this.headers, params: params });
    }

    public getByTreatmentId(params: any): Observable<PaginatedListModel<TreatmentDetailModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<TreatmentDetailModel>>(`${this.treatmentDetailUrl}/GetByTreatmentId/${params.treatmentId}`, { headers: this.headers, params: params });
    }

    public getDetail(id: any): Observable<TreatmentDetailModel> {
        return this.http.get<TreatmentDetailModel>(this.treatmentDetailUrl + '/' + id, { headers: this.headers });
    }

    public add(patientProfileModel: TreatmentDetailModel): Observable<number> {
        return this.http.post<number>(this.treatmentDetailUrl, patientProfileModel, { headers: this.headers});
    }

    public update(params: TreatmentDetailModel): Observable<boolean> {
        return this.http.put<boolean>(this.treatmentDetailUrl + '/' + params.id, params, { headers: this.headers});
    }

    public delete(id: number) {
        return this.http.delete(this.treatmentDetailUrl + '/' + id, { headers: this.headers});
    }
}
