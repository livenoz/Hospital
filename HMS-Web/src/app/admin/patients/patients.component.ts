import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { faBriefcase } from '@fortawesome/fontawesome-free-solid';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from '../../shared/modules/modal/modal-component';
import { Constants } from '../../shared/constants/constants';
import { PatientService } from './shared/patient.service';
import { PatientModel } from './shared/patient.model';
import { PaginatedListModel } from '../../shared/models/paginated-list.model';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})
export class PatientsComponent implements OnInit {
  constructor(
    private _spinner: NgxSpinnerService,
    private _modal: NgbModal,
    private _patientService: PatientService) {
  }

  // icon
  public faBriefcase = faBriefcase;

  // Search AutoComplete
  public searchControl = new FormControl();
  public filteredOptions: Observable<string[]>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public options: string[] = [];

  // Data list [Start]
  // tslint:disable-next-line:member-ordering
  public displayedColumns: string[] = [
    'Code',
    'FullName',
    'IdentifyCardNo',
    'Phone',
    'Address',
    'Sex',
    'control'
  ];
  public test: number;
  public dataSource = new MatTableDataSource<PatientModel>();

  public ngOnInit() {
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = 2;
    this.searchConfig();
  }

  public searchConfig() {
    this._spinner.show();
    this.filteredOptions = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
    this.dataSource.paginator = this.paginator;
    const params = {
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize
    };
    this._patientService.get(params).subscribe(
      this.onGetNext,
      this.onGetError,
      this.onGetComplete
    );
  }

  private onGetError = (err) => {
    console.log(err);
    this._spinner.hide();
  }

  private onGetComplete = () => {
    this._spinner.hide();
  }

  private onGetNext = (data: PaginatedListModel<PatientModel>) => {
    this.options = data.items.map(x => x.fullName);
    this.dataSource.data = data.items;
    this.test = data.totalItems;
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option =>
      option.toLowerCase().includes(filterValue)
    );
  }

  public onPageChange() {
    this.searchConfig();
  }

  // Data list [End]

  public onUpdate() {
    alert('Clicked');
  }

  public onDisable() {
    const modalRef = this._modal.open(NgbdModalComponent);
    modalRef.componentInstance.header = Constants.MODAL.DISABLE_HEADER;
    modalRef.componentInstance.content = Constants.MODAL.DISABLE_CONTENT;
    modalRef.componentInstance.isDisplayCancel = true;
    modalRef.result.then(result => {
      alert(result);
    });
  }

  public onViewTreatment() {
    alert('Clicked');
  }
}
