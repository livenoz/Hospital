import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constants } from '../constants/constants';
import { PaginatedListModel } from '../models/paginated-list.model';
import { CountryModel } from '../models/address/country.model';

@Injectable()
export class AddressService {
    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        const token = localStorage.getItem('accessToken');
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8',
            'Authorization': 'Bearer ' + token
        });
    }

    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    countryUrl: string = this.baseUrl + Constants.API_ADDRESSES.country;

    public getAllCountries(): Observable<PaginatedListModel<CountryModel>> {
        const params = {
            pageIndex: 0,
            pageSize: 1000
        };
        return this.getCountries(params);
    }

    public getCountries(params: any): Observable<PaginatedListModel<CountryModel>> {
        return this.http.get<PaginatedListModel<CountryModel>>(this.countryUrl, { headers: this.headers, params: params });
    }
}
