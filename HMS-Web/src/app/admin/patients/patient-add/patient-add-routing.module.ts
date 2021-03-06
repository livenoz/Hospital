import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PatientAddComponent } from './patient-add.component';

const routes: Routes = [
    {
        path: '',
        component: PatientAddComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PatientAddRoutingModule {}
