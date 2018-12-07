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
            { path: 'patient-profiles', loadChildren: './patient-profiles/patient-profiles.module#PatientProfilesModule' },
            { path: 'add-update-patient-profiles', loadChildren: './patient-profiles/add-update-patient-profiles/add-update-patient-profiles.module#AddUpdatePatientProfilesModule' },
            { path: 'add-update-patient-profiles/:id', loadChildren: './patient-profiles/add-update-patient-profiles/add-update-patient-profiles.module#AddUpdatePatientProfilesModule' },
            { path: 'treatment', loadChildren: './treatment/treatment.module#TreatmentModule' },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule {}
