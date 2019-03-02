import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';

const routes: Routes = [
    {
        path: '',
        component: AdminComponent,
        children: [
            { path: '', redirectTo: 'dashboard', pathMatch: 'prefix' },
            { path: 'dashboard', loadChildren: './dashboard/dashboard.module#DashboardModule' },
            { path: 'patients', loadChildren: './patients/patients.module#PatientsModule' },
            { path: 'patient-add', loadChildren: './patients/patient-add/patient-add.module#PatientAddModule' },
            { path: 'patient-update/:id', loadChildren: './patients/patient-update/patient-update.module#PatientUpdateModule' },
            { path: 'patient-detail/:id', loadChildren: './patients/patient-detail/patient-detail.module#PatientDetailModule' },
            { path: 'medical-records', loadChildren: './medical-records/medical-records.module#MedicalRecordsModule' },
            // tslint:disable-next-line:max-line-length
            { path: 'medical-record-add', loadChildren: './medical-records/medical-record-add/medical-record-add.module#MedicalRecordAddModule' },
            // tslint:disable-next-line:max-line-length
            { path: 'medical-record-update/:id', loadChildren: './medical-records/medical-record-update/medical-record-update.module#MedicalRecordUpdateModule' },
            // { path: 'treatments', loadChildren: './treatments/treatments.module#TreatmentModule' },
            { path: 'treatment-add/:medicalRecordId', loadChildren: './treatments/treatment-add/treatment-add.module#TreatmentAddModule' },
            { path: 'treatment-update/:id', loadChildren: './treatments/treatment-update/treatment-update.module#TreatmentUpdateModule' },
            { path: 'treatments/:medicalRecordId', loadChildren: './treatments/treatments.module#TreatmentModule' },
            // tslint:disable-next-line:max-line-length
            { path: 'treatment-detail-add/:treatmentId', loadChildren: './treatment-details/treatment-detail-add/treatment-detail-add.module#TreatmentDetailAddModule' },
            // tslint:disable-next-line:max-line-length
            { path: 'treatment-detail-update/:id', loadChildren: './treatment-details/treatment-detail-update/treatment-detail-update.module#TreatmentDetailUpdateModule' },
            { path: 'treatment-details/:treatmentId', loadChildren: './treatment-details/treatment-details.module#TreatmentDetailsModule' },
            // tslint:disable-next-line:max-line-length
            { path: 'prescriptions/:treatmentId/:treatmentDetailId', loadChildren: './prescriptions/prescriptions.module#PrescriptionsModule' },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule {}
