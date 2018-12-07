import {Component} from '@angular/core';
import {NgbDatepickerConfig, NgbCalendar, NgbDate, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'ngbd-datepicker-config',
  templateUrl: './datepicker-config.html',
  providers: [NgbDatepickerConfig] // add NgbDatepickerConfig to the component providers
})
export class NgbdDatepickerConfig {

  model: NgbDateStruct;

  constructor(config: NgbDatepickerConfig, calendar: NgbCalendar) {
    // customize default values of datepickers used by this component tree
    config.minDate = {year: 1900, month: 1, day: 1};
    config.maxDate = {year: 2099, month: 12, day: 31};
  }
}
