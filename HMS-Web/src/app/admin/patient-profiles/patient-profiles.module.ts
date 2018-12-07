import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PatientProfilesRoutingModule } from "./patient-profiles-routing.module";
import { PatientProfilesComponent } from "./patient-profiles.component";
import { PageHeaderModule } from "./../../shared";
import { MatTableModule } from "@angular/material";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule  } from "@angular/material";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  imports: [
    CommonModule,
    PatientProfilesRoutingModule,
    PageHeaderModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatPaginatorModule,
    FontAwesomeModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule
  ],
  declarations: [PatientProfilesComponent],
})
export class PatientProfilesModule {}
