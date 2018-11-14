import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AddUpdatePatientProfilesRoutingModule } from "./add-update-patient-profiles-routing.module";
import { AddUpdatePatientProfilesComponent } from "./add-update-patient-profiles.component";
import { PageHeaderModule } from "./../../../shared";

@NgModule({
  imports: [
    CommonModule,
    AddUpdatePatientProfilesRoutingModule,
    PageHeaderModule
  ],
  declarations: [AddUpdatePatientProfilesComponent]
})
export class PatientProfilesModule {}
