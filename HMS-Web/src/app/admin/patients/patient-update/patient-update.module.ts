import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PatientUpdateRoutingModule } from './patient-update-routing.module';
import { PatientUpdateComponent } from './patient-update.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// tslint:disable-next-line:max-line-length
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule  } from '@angular/material';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

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
    NgbModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule
  ],
  declarations: [PatientUpdateComponent]
})
export class PatientUpdateModule {}
