import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MedicalRecordUpdateComponent } from './medical-record-update.component';

const routes: Routes = [
    {
        path: '',
        component: MedicalRecordUpdateComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MedicalRecordUpdateRoutingModule {}
