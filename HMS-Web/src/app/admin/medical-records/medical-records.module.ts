import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MedicalRecordsRoutingModule } from './medical-records-routing.module';
import { MedicalRecordsComponent } from './medical-records.component';
import { PageHeaderModule } from '../../shared';
import { MatTableModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule  } from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  imports: [
    CommonModule,
    MedicalRecordsRoutingModule,
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
  declarations: [MedicalRecordsComponent],
})
export class MedicalRecordsModule {}
