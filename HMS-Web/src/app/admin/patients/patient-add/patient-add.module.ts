import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PatientAddRoutingModule } from './patient-add-routing.module';
import { PatientAddComponent } from './patient-add.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule  } from '@angular/material';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerConfig } from '../../../shared/datepicker/datepicker-config';

@NgModule({
  imports: [
    CommonModule,
    PatientAddRoutingModule,
    PageHeaderModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatPaginatorModule,
    NgbModule
  ],
  declarations: [PatientAddComponent, NgbdDatepickerConfig]
})
export class PatientAddModule {}
