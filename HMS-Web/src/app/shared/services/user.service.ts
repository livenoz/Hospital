import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Constants } from '../constants/constants';

@Injectable()
export class UserService {
    private headers: HttpHeaders;

    constructor (private http: HttpClient) {
        this.headers = new HttpHeaders({
            'Content-Type': 'application/json; charset=utf-8'
        });
    }
    baseUrl: string = Constants.BASE_URL + Constants.API_URL + Constants.API_VERSION_1;
    loginUrl: string = this.baseUrl + Constants.API_USERS.login;

    login(username: string, password: string) {
        const user = {'Username': username, 'password': password};
        return this.http.post(this.loginUrl, user, {
            headers: this.headers
        });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('accessToken');
    }
}
