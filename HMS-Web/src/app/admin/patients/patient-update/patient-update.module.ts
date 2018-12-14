import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PatientUpdateRoutingModule } from './patient-update-routing.module';
import { PatientUpdateComponent } from './patient-update.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule  } from '@angular/material';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbdDatepickerConfig } from '../../../shared/datepicker/datepicker-config';

@NgModule({
  imports: [
    CommonModule,
    PatientUpdateRoutingModule,
    PageHeaderModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatPaginatorModule,
    NgbModule
  ],
  declarations: [PatientUpdateComponent, NgbdDatepickerConfig]
})
export class PatientUpdateModule {}
