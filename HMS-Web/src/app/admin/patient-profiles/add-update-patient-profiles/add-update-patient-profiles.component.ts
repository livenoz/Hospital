import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";

@Component({
  selector: 'app-add-update-patient-profiles',
  templateUrl: './add-update-patient-profiles.component.html',
  styleUrls: ['./add-update-patient-profiles.component.scss']
})
export class AddUpdatePatientProfilesComponent implements OnInit {

  constructor(private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.spinner.hide();
  }

}
