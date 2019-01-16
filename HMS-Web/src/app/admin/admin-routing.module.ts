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
            { path: 'medical-records', loadChildren: './medical-records/medical-records.module#MedicalRecordsModule' },
            // tslint:disable-next-line:max-line-length
            { path: 'medical-record-add', loadChildren: './medical-records/medical-record-add/medical-record-add.module#MedicalRecordAddModule' },
            // tslint:disable-next-line:max-line-length
            { path: 'medical-record-update/:id', loadChildren: './medical-records/medical-record-update/medical-record-update.module#MedicalRecordUpdateModule' },
            { path: 'treatments', loadChildren: './treatments/treatments.module#TreatmentModule' },
            { path: 'treatment-add', loadChildren: './treatments/treatment-add/treatment-add.module#TreatmentAddModule' },
            { path: 'treatment-update/:id', loadChildren: './treatments/treatment-update/treatment-update.module#TreatmentUpdateModule' },
            { path: 'treatments/:patientId', loadChildren: './treatments/treatments.module#TreatmentModule' },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule {}
