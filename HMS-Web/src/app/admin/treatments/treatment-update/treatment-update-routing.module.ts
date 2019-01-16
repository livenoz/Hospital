import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TreatmentUpdateComponent } from './treatment-update.component';

const routes: Routes = [
    {
        path: '',
        component: TreatmentUpdateComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TreatmentUpdateRoutingModule {}
