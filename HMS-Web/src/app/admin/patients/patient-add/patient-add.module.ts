import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PatientAddRoutingModule } from './patient-add-routing.module';
import { PatientAddComponent } from './patient-add.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// tslint:disable-next-line:max-line-length
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule  } from '@angular/material';
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
    NgbModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule
  ],
  declarations: [PatientAddComponent, NgbdDatepickerConfig]
})
export class PatientAddModule {}
