import { Component, OnInit  } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "ngbd-modal-component",
  templateUrl: "./modal-component.html"
})

export class NgbdModalComponent implements OnInit {
    withAutoComponent = `<button type="button" ngbAutocomponent class="btn btn-danger"
        (click)="modal.close('Ok click')">Ok</button>`;
    constructor(public modal: NgbActiveModal) {}

    ngOnInit() {
    }
}