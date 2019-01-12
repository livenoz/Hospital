import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constants } from '../constants/constants';
import { PaginatedListModel } from '../models/paginated-list.model';
import { NurseModel } from '../models/employee/doctor.model';
import { DoctorModel } from '../models/employee/nurse.model';

@Injectable()
export class EmployeeService {
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        const token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    doctorUrl: string = this.baseUrl + Constants.API_EMPLOYEE.doctor;
    nurseUrl: string = this.baseUrl + Constants.API_EMPLOYEE.nurse;

    public getAllDoctors(): Observable<PaginatedListModel<DoctorModel>> {
        const params = {
            pageIndex: 0,
            pageSize: 1000
        };
        return this.getDoctors(params);
    }

    public getDoctors(params: any): Observable<PaginatedListModel<DoctorModel>> {
        return this.http.get<PaginatedListModel<DoctorModel>>(this.doctorUrl, { headers: this.headers, params: params });
    }

    public getAllNurses(): Observable<PaginatedListModel<NurseModel>> {
        const params = {
            pageIndex: 0,
            pageSize: 1000
        };
        return this.getNurses(params);
    }

    public getNurses(params: any): Observable<PaginatedListModel<NurseModel>> {
        return this.http.get<PaginatedListModel<NurseModel>>(this.nurseUrl, { headers: this.headers, params: params });
    }
}
