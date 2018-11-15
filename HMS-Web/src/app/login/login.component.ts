import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    constructor(public router: Router, private spinner: NgxSpinnerService) {}

    ngOnInit() {
        this.spinner.hide();
    }

    onLoggedin() {
        localStorage.setItem('isLoggedin', 'true');
    }
}
