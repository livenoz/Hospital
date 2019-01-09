import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../../../shared/constants/constants';
import { Observable } from 'rxjs';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { MedicalRecordModel } from './medical-record.model';

@Injectable()
export class MedicalRecordService {
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        const token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    medicalRecordUrl: string = this.baseUrl + Constants.API_MEDICAL_RECORDS.medicalRecord;

    public get(params: any): Observable<PaginatedListModel<MedicalRecordModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<MedicalRecordModel>>(this.medicalRecordUrl, { headers: this.headers, params: params });
    }

    public getDetail(id: any): Observable<MedicalRecordModel> {
        return this.http.get<MedicalRecordModel>(this.medicalRecordUrl + '/' + id, { headers: this.headers });
    }

    public add(patientProfileModel: MedicalRecordModel): Observable<number> {
        return this.http.post<number>(this.medicalRecordUrl, patientProfileModel, { headers: this.headers});
    }

    public update(params: MedicalRecordModel): Observable<boolean> {
        return this.http.put<boolean>(this.medicalRecordUrl + '/' + params.id, params, { headers: this.headers});
    }

    public delete(id: number) {
        return this.http.delete(this.medicalRecordUrl + '/' + id, { headers: this.headers});
    }
}
