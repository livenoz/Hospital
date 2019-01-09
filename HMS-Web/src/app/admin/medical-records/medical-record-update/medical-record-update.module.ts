import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MedicalRecordUpdateRoutingModule } from './medical-record-update-routing.module';
import { MedicalRecordUpdateComponent } from './medical-record-update.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// tslint:disable-next-line:max-line-length
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule, MatSelectModule  } from '@angular/material';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    MedicalRecordUpdateRoutingModule,
    PageHeaderModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatPaginatorModule,
    NgbModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    MatSelectModule
  ],
  declarations: [MedicalRecordUpdateComponent]
})
export class MedicalRecordUpdateModule {}
