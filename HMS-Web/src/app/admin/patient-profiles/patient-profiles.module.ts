import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PatientProfilesRoutingModule } from "./patient-profiles-routing.module";
import { PatientProfilesComponent } from "./patient-profiles.component";
import { PageHeaderModule } from "./../../shared";
import { MatTableModule } from "@angular/material";
import { MatCheckboxModule } from "@angular/material";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatPaginatorIntl  } from "@angular/material";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgbdModalComponent } from './../../shared/modules/modal/modal-component';
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

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
    FontAwesomeModule,
    NgbModule
  ],
  declarations: [PatientProfilesComponent, NgbdModalComponent],
  entryComponents: [NgbdModalComponent],
  providers: [NgbActiveModal]
})
export class PatientProfilesModule {}
