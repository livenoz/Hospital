import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Constants } from '../constants/Constants';

@Injectable()
export class PatientService{
    private headers: HttpHeaders;

    constructor (private http: HttpClient){
        let token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            "Content-Type": "application/json; charset=utf-8",
            "Authorization": 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    patientUrl: string = this.baseUrl + Constants.API_PATIENTS.patient;

    public get(params: any) {
        return this.http.get(this.patientUrl, { headers: this.headers, params: params });
    }

    public getDetail(id: any) {
        return this.http.get(this.patientUrl, { headers: this.headers, params: id });
    }

    public add(patientProfileModel: any) {
        return this.http.post(this.patientUrl, { headers: this.headers, params: patientProfileModel });
    }

    public update(patientProfileModel: any) {
        return this.http.put(this.patientUrl, { headers: this.headers, params: patientProfileModel });
    }

    public delete(id: any) {
        return this.http.delete(this.patientUrl, { headers: this.headers, params: id });
    }
}