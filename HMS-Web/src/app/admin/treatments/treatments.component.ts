import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, PageEvent } from '@angular/material';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { faBriefcase } from '@fortawesome/fontawesome-free-solid';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from '../../shared/modules/modal/modal-component';
import { Constants } from '../../shared/constants/constants';
import { PaginatedListModel } from '../../shared/models/paginated-list.model';
import { AddressService } from '../../shared/services/address.service';
import { PatientService } from '../patients/shared/patient.service';
import { PatientFilter } from '../patients/shared/patient-filter.model';
import { PatientModel } from '../patients/shared/patient.model';

@Component({
  selector: 'app-treatments',
  templateUrl: './treatments.component.html',
  styleUrls: ['./treatments.component.scss']
})
export class TreatmentsComponent implements OnInit {
  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private patientService: PatientService,
    private addressService: AddressService) {
  }

  // icon
  public faBriefcase = faBriefcase;
  pageEvent: PageEvent;
  // Search AutoComplete
  public searchControl = new FormControl();
  public filteredOptions: Observable<string[]>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public options: string[] = [];
  public filter: PatientFilter = new PatientFilter();

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
    this.addressService.init();
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = 10;
    // this.dataSource.paginator = this.paginator;
    this.filter.code = 'code';
    this.filter.value = '';
    this.searchConfig();
  }

  public searchConfig() {
    this.spinner.show();
    this.filteredOptions = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );
    const params = {
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize,
      code: this.filter.code,
      value: this.filter.value,
    };
    this.patientService.get(params).subscribe(
      this.onGetNext,
      this.onGetError,
      this.onGetComplete
    );
  }

  private onGetError = (err) => {
    console.log(err);
    this.spinner.hide();
  }

  private onGetComplete = () => {
    this.spinner.hide();
  }

  private onGetNext = (data: PaginatedListModel<PatientModel>) => {
    this.options = data.items.map(x => x.fullName);
    this.dataSource.data = data.items;
    this.paginator.length = data.totalItems;
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option =>
      option.toLowerCase().includes(filterValue)
    );
  }

  public onPageChange(event: PageEvent) {
    this.pageEvent = event;
    this.searchConfig();
  }

  // Data list [End]

  public onUpdate() {
    alert('Clicked');
  }

  public onDisable() {
    const modalRef = this.modal.open(NgbdModalComponent);
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
