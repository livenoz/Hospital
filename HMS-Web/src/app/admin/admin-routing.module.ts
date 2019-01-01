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
            { path: 'treatments', loadChildren: './treatments/treatments.module#TreatmentModule' },
            { path: 'treatments/:patientId', loadChildren: './treatments/treatments.module#TreatmentModule' },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule {}
