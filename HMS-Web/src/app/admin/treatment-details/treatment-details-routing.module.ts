import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TreatmentDetailsComponent } from './treatment-details.component';

const routes: Routes = [
    {
        path: '',
        component: TreatmentDetailsComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TreatmentDetailsRoutingModule {}
