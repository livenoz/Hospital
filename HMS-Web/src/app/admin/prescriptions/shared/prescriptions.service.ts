import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../../../shared/constants/constants';
import { Observable } from 'rxjs';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { DrugModel } from './drug.model';
import { UnitModel } from './unit.model';
import { PrescriptionDetailModel } from './prescription-detail.model';

@Injectable()
export class PrescriptionsService {
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        const token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    prescriptionUrl: string = this.baseUrl + Constants.API_PRESCRIPTIONS.prescription;
    drugUrl: string = this.baseUrl + Constants.API_PRESCRIPTIONS.drug;
    unitUrl: string = this.baseUrl + Constants.API_PRESCRIPTIONS.unit;

    public get(params: any): Observable<PaginatedListModel<PrescriptionDetailModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<PrescriptionDetailModel>>(this.prescriptionUrl, { headers: this.headers, params: params });
    }

    public getByTreatmentDetailId(params: any): Observable<PaginatedListModel<PrescriptionDetailModel>> {
        // tslint:disable-next-line:max-line-length
        return this.http.get<PaginatedListModel<PrescriptionDetailModel>>(`${this.prescriptionUrl}/ByTreatmentDetailId`, { headers: this.headers, params: params });
    }

    public getDrugs(params: any): Observable<PaginatedListModel<DrugModel>> {
        return this.http.get<PaginatedListModel<DrugModel>>(this.drugUrl, { headers: this.headers, params: params });
    }

    public getUnits(params: any): Observable<PaginatedListModel<UnitModel>> {
        return this.http.get<PaginatedListModel<UnitModel>>(this.unitUrl, { headers: this.headers, params: params });
    }

    // public getDetail(id: any): Observable<TreatmentDiseaseModel> {
    //     return this.http.get<TreatmentDiseaseModel>(this.treatmentUrl + '/' + id, { headers: this.headers });
    // }

    // public add(patientProfileModel: TreatmentDiseaseModel): Observable<number> {
    //     return this.http.post<number>(this.treatmentUrl, patientProfileModel, { headers: this.headers});
    // }

    public update(params: any): Observable<boolean> {
        return this.http.put<boolean>(this.prescriptionUrl + '/' + params.treatmentDetailId, params, { headers: this.headers});
    }

    public delete(id: number) {
        return this.http.delete(this.prescriptionUrl + '/' + id, { headers: this.headers});
    }

    public getAllUnits(): Observable<PaginatedListModel<UnitModel>> {
        const params = {
            pageIndex: 0,
            pageSize: 1000
        };
        return this.getUnits(params);
    }

    public getAllDrugs(): Observable<PaginatedListModel<DrugModel>> {
        const params = {
            pageIndex: 0,
            pageSize: 1000
        };
        return this.getDrugs(params);
    }
}
