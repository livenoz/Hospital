import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddUpdatePatientProfilesComponent } from './add-update-patient-profiles.component';

const routes: Routes = [
    {
        path: '',
        component: AddUpdatePatientProfilesComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AddUpdatePatientProfilesRoutingModule {}
