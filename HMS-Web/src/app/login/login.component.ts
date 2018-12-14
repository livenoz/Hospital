import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from '../shared/services/user.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    constructor(public router: Router, private spinner: NgxSpinnerService, private userService: UserService) { }

    user = {'username': '', 'password': ''};
    ngOnInit() {
        this.spinner.hide();
    }

    onLoggedin() {
        this.userService.login(this.user.username, this.user.password).subscribe((data: any) => {
            if (data && data.accessToken) {
                localStorage.setItem('accessToken', data.accessToken);
                this.router.navigate(['/dashboard']);
            }
        });
    }
}
