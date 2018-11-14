import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { PatientProfilesRoutingModule } from "./patient-profiles-routing.module";
import { PatientProfilesComponent } from "./patient-profiles.component";
import { PageHeaderModule } from "./../../shared";
import { MatTableModule } from "@angular/material";
import { MatCheckboxModule } from "@angular/material";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatPaginatorIntl  } from "@angular/material";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  imports: [
    CommonModule,
    PatientProfilesRoutingModule,
    PageHeaderModule,
    MatTableModule,
    MatCheckboxModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatPaginatorModule,
    FontAwesomeModule
  ],
  declarations: [PatientProfilesComponent]
})
export class PatientProfilesModule {}
