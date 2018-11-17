import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PatientProfilesComponent } from './patient-profiles.component';

const routes: Routes = [
    {
        path: '',
        component: PatientProfilesComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PatientProfilesRoutingModule {}
