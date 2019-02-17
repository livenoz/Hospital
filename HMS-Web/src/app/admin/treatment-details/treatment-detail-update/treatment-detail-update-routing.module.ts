import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TreatmentDetailUpdateComponent } from './treatment-detail-update.component';

const routes: Routes = [
    {
        path: '',
        component: TreatmentDetailUpdateComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TreatmentDetailUpdateRoutingModule {}
