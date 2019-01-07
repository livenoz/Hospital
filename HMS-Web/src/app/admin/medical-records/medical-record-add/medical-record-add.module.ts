import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MedicalRecordAddRoutingModule } from './medical-record-add-routing.module';
import { MedicalRecordAddComponent } from './medical-record-add.component';
import { PageHeaderModule } from '../../../shared';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// tslint:disable-next-line:max-line-length
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule, MatDatepickerModule, MatNativeDateModule, MatRadioModule, MatProgressSpinnerModule, MatSelectModule  } from '@angular/material';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { OptionsScrollDirective } from '../../../shared/autocomplete/options-scroll.directive';

@NgModule({
  imports: [
    CommonModule,
    MedicalRecordAddRoutingModule,
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
  declarations: [MedicalRecordAddComponent, OptionsScrollDirective]
})
export class MedicalRecordAddModule {}
