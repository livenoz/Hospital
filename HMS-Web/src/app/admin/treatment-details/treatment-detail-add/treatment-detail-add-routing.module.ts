import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TreatmentDetailAddComponent } from './treatment-detail-add.component';

const routes: Routes = [
    {
        path: '',
        component: TreatmentDetailAddComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TreatmentDetailAddRoutingModule {}
