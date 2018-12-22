import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constants } from '../constants/constants';
import { PaginatedListModel } from '../models/paginated-list.model';
import { CountryModel } from '../models/address/country.model';
import { ProvinceModel } from '../models/address/province.model';
import { DistrictModel } from '../models/address/district.model';

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
    provinceUrl: string = this.baseUrl + Constants.API_ADDRESSES.province;
    districtUrl: string = this.baseUrl + Constants.API_ADDRESSES.district;
    countries: CountryModel[];
    provinces: ProvinceModel[];
    districts: DistrictModel[];

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


    public getAllProvinces(): Observable<PaginatedListModel<ProvinceModel>> {
        const params = {
            pageIndex: 0,
            pageSize: 1000
        };
        return this.getProvinces(params);
    }

    public getProvinces(params: any): Observable<PaginatedListModel<ProvinceModel>> {
        return this.http.get<PaginatedListModel<ProvinceModel>>(this.provinceUrl, { headers: this.headers, params: params });
    }


    public getAllDistricts(): Observable<PaginatedListModel<DistrictModel>> {
        const params = {
            pageIndex: 0,
            pageSize: 1000
        };
        return this.getDistricts(params);
    }

    public getDistricts(params: any): Observable<PaginatedListModel<DistrictModel>> {
        return this.http.get<PaginatedListModel<DistrictModel>>(this.districtUrl, { headers: this.headers, params: params });
    }

    public init() {
        this.getAllCountries().subscribe(
            (data: PaginatedListModel<CountryModel>) => {
                this.countries = data.items;
            },
            (error: any) => { },
            () => { }
        );
        this.getAllProvinces().subscribe(
            (data: PaginatedListModel<ProvinceModel>) => {
                this.provinces = data.items;
            },
            (error: any) => { },
            () => { }
        );
        this.getAllDistricts().subscribe(
            (data: PaginatedListModel<DistrictModel>) => {
                this.districts = data.items;
            },
            (error: any) => { },
            () => { }
        );
    }
}
