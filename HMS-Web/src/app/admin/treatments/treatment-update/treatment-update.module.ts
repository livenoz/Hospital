import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TreatmentUpdateRoutingModule } from './treatment-update-routing.module';
import { TreatmentUpdateComponent } from './treatment-update.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// tslint:disable-next-line:max-line-length
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule, MatProgressSpinnerModule, MatSelectModule  } from '@angular/material';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    TreatmentUpdateRoutingModule,
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
  declarations: [TreatmentUpdateComponent]
})
export class TreatmentUpdateModule {}
