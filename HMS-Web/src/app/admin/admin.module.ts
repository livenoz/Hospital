import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { HeaderComponent } from './components/header/header.component';
import { NgbdModalComponent } from './../shared/modules/modal/modal-component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    imports: [
        CommonModule,
        AdminRoutingModule,
        NgbDropdownModule.forRoot()
    ],
    declarations: [AdminComponent, SidebarComponent, HeaderComponent, NgbdModalComponent],
    entryComponents: [NgbdModalComponent],
    providers: [NgbActiveModal]
})
export class AdminModule {}
