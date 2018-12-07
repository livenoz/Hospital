import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TreatmentComponent } from './treatment.component';

const routes: Routes = [
    {
        path: '',
        component: TreatmentComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TreatmentRoutingModule {}
