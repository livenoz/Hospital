import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    public alerts: Array<any> = [];
    public sliders: Array<any> = [];

    constructor(private spinner: NgxSpinnerService) {
        this.sliders.push(
            {
                imagePath: 'assets/images/slider1.jpg',
                label: 'Welcome to HSM',
                text:
                    'Healthcare Management System.'
            }
        );
    }

    ngOnInit() {
        this.spinner.hide();
    }
}
