import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TreatmentsRoutingModule } from './treatments-routing.module';
import { TreatmentsComponent } from './treatments.component';
import { PageHeaderModule } from '../../shared';
import { MatTableModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule, MatInputModule, MatPaginatorModule  } from '@angular/material';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@NgModule({
  imports: [
    CommonModule,
    TreatmentsRoutingModule,
    PageHeaderModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatInputModule,
    MatPaginatorModule,
    FontAwesomeModule,
    NgbModule,
    ReactiveFormsModule
  ],
  declarations: [TreatmentsComponent]
})
export class TreatmentModule {}
