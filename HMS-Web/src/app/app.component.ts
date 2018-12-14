import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private spinner: NgxSpinnerService, private router: Router) {
    this.router.events.subscribe((event) => {
      this.spinner.show();
    });
  }

  ngOnInit() {}
}
