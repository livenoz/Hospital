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
import { MedicalRecordService } from './shared/medical-record.service';
import { MedicalRecordModel } from './shared/medical-record.model';
import { PaginatedListModel } from '../../shared/models/paginated-list.model';
import { AddressService } from '../../shared/services/address.service';

@Component({
  selector: 'app-medical-records',
  templateUrl: './medical-records.component.html',
  styleUrls: ['./medical-records.component.scss']
})
export class MedicalRecordsComponent implements OnInit {
  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private medicalRecordService: MedicalRecordService,
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

  // Data list [Start]
  // tslint:disable-next-line:member-ordering
  public displayedColumns: string[] = [
    'PatientFullName',
    'StartDate',
    'EndDate',
    'Reason',
    'StatusName',
    'control'
  ];
  public test: number;
  public dataSource = new MatTableDataSource<MedicalRecordModel>();

  public ngOnInit() {
    this.addressService.init();
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = 10;
    // this.dataSource.paginator = this.paginator;
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
    };
    this.medicalRecordService.get(params).subscribe(
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

  private onGetNext = (data: PaginatedListModel<MedicalRecordModel>) => {
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
}
