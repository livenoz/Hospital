import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PatientUpdateComponent } from './patient-update.component';

const routes: Routes = [
    {
        path: '',
        component: PatientUpdateComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class PatientUpdateRoutingModule {}
