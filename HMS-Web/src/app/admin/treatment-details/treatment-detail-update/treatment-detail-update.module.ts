import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TreatmentDetailUpdateRoutingModule } from './treatment-detail-update-routing.module';
import { TreatmentDetailUpdateComponent } from './treatment-detail-update.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// tslint:disable-next-line:max-line-length
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule, MatProgressSpinnerModule, MatSelectModule  } from '@angular/material';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    TreatmentDetailUpdateRoutingModule,
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
    MatProgressSpinnerModule,
    MatSelectModule
  ],
  declarations: [TreatmentDetailUpdateComponent]
})
export class TreatmentDetailUpdateModule {}
