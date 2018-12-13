import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../constants/constants';
import { Observer, Observable } from 'rxjs';
import { PatientsResponse } from '../models/patient/PatientsResponse';

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

    public get(params: any): Observable<PatientsResponse> {
        return this.http.get<PatientsResponse>(this.patientUrl, { headers: this.headers, params: params });
    }

    public getDetail(id: any) {
        return this.http.get(this.patientUrl, { headers: this.headers, params: id });
    }

    public add(patientProfileModel: any) {
        return this.http.post(this.patientUrl, { headers: this.headers, params: patientProfileModel });
    }

    public update(id: any) {
        return this.http.put(this.patientUrl, { headers: this.headers, params: id });
    }

    public delete(id: any) {
        return this.http.delete(this.patientUrl, { headers: this.headers, params: id });
    }
}
