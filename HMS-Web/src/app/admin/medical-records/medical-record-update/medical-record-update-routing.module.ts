import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MedicalRecordComponent } from './medical-record-update.component';

const routes: Routes = [
    {
        path: '',
        component: MedicalRecordComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MedicalRecordUpdateRoutingModule {}
