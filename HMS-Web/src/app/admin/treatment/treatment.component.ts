import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, map } from 'rxjs/operators';

const codes = ['HSM00001', 'HSM00002', 'HSM00003', 'HSM00004', 'HSM00005'];

@Component({
  selector: 'app-treatment',
  templateUrl: './treatment.component.html',
  styleUrls: ['./treatment.component.scss'],
  styles: [`.dropdown-menu { width: 300px; }`]
})
export class TreatmentComponent implements OnInit {
  public searchModel: any;

  constructor(private spinner: NgxSpinnerService) { }

  ngOnInit() {
    this.spinner.hide();
  }

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map(term => term.length < 1 ? []
        : codes.filter(v => v.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10))
    )
}
