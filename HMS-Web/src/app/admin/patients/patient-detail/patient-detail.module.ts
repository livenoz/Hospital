import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PatientDetailRoutingModule } from './patient-detail-routing.module';
import { PatientDetailComponent } from './patient-detail.component';
import { MatTableModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule  } from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PageHeaderModule } from '../../../shared';

@NgModule({
  imports: [
    CommonModule,
    PatientDetailRoutingModule,
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
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule
  ],
  declarations: [PatientDetailComponent],
})
export class PatientDetailModule {}
