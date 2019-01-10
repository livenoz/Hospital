import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TreatmentAddComponent } from './treatment-add.component';

const routes: Routes = [
    {
        path: '',
        component: TreatmentAddComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TreatmentAddRoutingModule {}
