import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../../../shared/constants/constants';
import { Observable } from 'rxjs';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { TreatmentModel } from './treatment.model';
import { DiseaseModel } from './disease.model';
import { TreatmentDetailModel } from './treatment-detail.model';

@Injectable()
export class TreatmentService {
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        const token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    treatmentUrl: string = this.baseUrl + Constants.API_TREATMENT.treatment;
    diseaseUrl: string = this.baseUrl + Constants.API_TREATMENT.disease;

    public get(params: any): Observable<PaginatedListModel<TreatmentModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<TreatmentModel>>(this.treatmentUrl, { headers: this.headers, params: params });
    }

    public getDiseases(params: any): Observable<PaginatedListModel<DiseaseModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<DiseaseModel>>(this.diseaseUrl, { headers: this.headers, params: params });
    }

    public getDetail(id: any): Observable<TreatmentDetailModel> {
        return this.http.get<TreatmentDetailModel>(this.treatmentUrl + '/' + id, { headers: this.headers });
    }

    public add(patientProfileModel: TreatmentDetailModel): Observable<number> {
        return this.http.post<number>(this.treatmentUrl, patientProfileModel, { headers: this.headers});
    }

    public update(params: TreatmentModel): Observable<boolean> {
        return this.http.put<boolean>(this.treatmentUrl + '/' + params.id, params, { headers: this.headers});
    }

    public delete(id: number) {
        return this.http.delete(this.treatmentUrl + '/' + id, { headers: this.headers});
    }
}
