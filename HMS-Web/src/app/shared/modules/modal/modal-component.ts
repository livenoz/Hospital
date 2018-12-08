import { Component, OnInit, Input, Output, EventEmitter  } from "@angular/core";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "ngbd-modal-component",
  templateUrl: "./modal-component.html"
})

export class NgbdModalComponent implements OnInit {
    @Input() header: string;
    @Input() content: string;
    @Input() isDisplayCancel: boolean;
    @Output() event: EventEmitter<any> = new EventEmitter();

    constructor(public modal: NgbActiveModal) {}

    ngOnInit() {
    }
}