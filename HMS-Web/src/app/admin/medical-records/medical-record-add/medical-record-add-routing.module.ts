import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MedicalRecordAddComponent } from './medical-record-add.component';

const routes: Routes = [
    {
        path: '',
        component: MedicalRecordAddComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class MedicalRecordAddRoutingModule {}
