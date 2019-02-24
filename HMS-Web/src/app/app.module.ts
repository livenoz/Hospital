import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthGuard } from './shared';
// Import spinner module
import { NgxSpinnerModule } from 'ngx-spinner';
import { UserService } from './shared/services/user.service';
import { PatientService } from './admin/patients/shared/patient.service';
import { AddressService } from './shared/services/address.service';
import { MedicalRecordService } from './admin/medical-records/shared/medical-record.service';
import { TreatmentService } from './admin/treatments/shared/treatment.service';
import { EmployeeService } from './shared/services/employee.service';
import { TreatmentDetailService } from './admin/treatment-details/shared/treatment-detail.service';
import { PrescriptionsService } from './admin/prescriptions/shared/prescriptions.service';


@NgModule({
  declarations: [AppComponent],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    NgxSpinnerModule
  ],
  providers: [
    AuthGuard,
    UserService,
    PatientService,
    AddressService,
    MedicalRecordService,
    TreatmentService,
    EmployeeService,
    TreatmentDetailService,
    PrescriptionsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
