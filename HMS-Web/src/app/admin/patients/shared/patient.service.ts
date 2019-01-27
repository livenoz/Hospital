import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../../../shared/constants/constants';
import { Observable } from 'rxjs';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { PatientModel } from './patient.model';

@Injectable()
export class PatientService {
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        const token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    patientUrl: string = this.baseUrl + Constants.API_PATIENTS.patient;

    public get(params: any): Observable<PaginatedListModel<PatientModel>> {
        return this.http.get<PaginatedListModel<PatientModel>>(this.patientUrl, { headers: this.headers, params: params });
    }

    public getDetail(id: any): Observable<PatientModel> {
        return this.http.get<PatientModel>(this.patientUrl + '/' + id, { headers: this.headers });
    }

    public add(patientProfileModel: PatientModel): Observable<number> {
        return this.http.post<number>(this.patientUrl, patientProfileModel, { headers: this.headers});
    }

    public update(params: PatientModel): Observable<boolean> {
        return this.http.put<boolean>(this.patientUrl + '/' + params.id, params, { headers: this.headers});
    }

    public active(params: any): Observable<boolean> {
        return this.http.put<boolean>(this.patientUrl + `/active?id=${params.id}&isActive=${params.isActive}`, { headers: this.headers});
    }

    public delete(id: number) {
        return this.http.delete(this.patientUrl + '/' + id, { headers: this.headers});
    }
}
