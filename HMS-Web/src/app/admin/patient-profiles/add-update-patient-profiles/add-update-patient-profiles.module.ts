import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AddUpdatePatientProfilesRoutingModule } from "./add-update-patient-profiles-routing.module";
import { AddUpdatePatientProfilesComponent } from "./add-update-patient-profiles.component";
import { PageHeaderModule } from "./../../../shared";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule  } from "@angular/material";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerConfig } from '../../../shared/datepicker/datepicker-config';

@NgModule({
  imports: [
    CommonModule,
    AddUpdatePatientProfilesRoutingModule,
    PageHeaderModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatPaginatorModule,
    NgbModule
  ],
  declarations: [AddUpdatePatientProfilesComponent, NgbdDatepickerConfig]
})
export class AddUpdatePatientProfilesModule {}
