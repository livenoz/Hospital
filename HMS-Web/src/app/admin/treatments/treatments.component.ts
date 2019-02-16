import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, PageEvent } from '@angular/material';
import { FormControl } from '@angular/forms';
import { faBriefcase } from '@fortawesome/fontawesome-free-solid';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from '../../shared/modules/modal/modal-component';
import { Constants } from '../../shared/constants/constants';
import { PaginatedListModel } from '../../shared/models/paginated-list.model';
import { TreatmentModel } from './shared/treatment.model';
import { TreatmentService } from './shared/treatment.service';
import { FilterModel } from '../../shared/models/filter.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-treatments',
  templateUrl: './treatments.component.html',
  styleUrls: ['./treatments.component.scss']
})
export class TreatmentsComponent implements OnInit {
  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private route: ActivatedRoute,
    private treatmentService: TreatmentService) {
  }

  // icon
  public faBriefcase = faBriefcase;
  pageEvent: PageEvent;
  // Search AutoComplete
  public searchControl = new FormControl();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  public options: string[] = [];
  public medicalRecordRouteId: number;

  // Data list [Start]
  // tslint:disable-next-line:member-ordering
  public displayedColumns: string[] = [
    'Code',
    'FullName',
    'MedicalRecordCode',
    'Title',
    'Content',
    'Doctor',
    'Nurse',
    'Note',
    'control'
  ];
  public test: number;
  public dataSource = new MatTableDataSource<TreatmentModel>();

  public ngOnInit() {
    this.medicalRecordRouteId = this.route.params['value'].medicalRecordId;
    this.paginator.pageIndex = 0;
    this.paginator.pageSize = 10;
    // this.dataSource.paginator = this.paginator;
    this.searchConfig();
  }

  public searchConfig() {
    this.spinner.show();
    const params = {
      pageIndex: this.paginator.pageIndex,
      pageSize: this.paginator.pageSize,
      medicalRecordId: this.medicalRecordRouteId
    };
    this.treatmentService.getByMedicalRecordId(params).subscribe(
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

  private onGetNext = (data: PaginatedListModel<TreatmentModel>) => {
    this.dataSource.data = data.items;
    this.paginator.length = data.totalItems;
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
    modalRef.componentInstance.content = Constants.MODAL.DISABLE_PATIENT_CONTENT;
    modalRef.componentInstance.isDisplayCancel = true;
    modalRef.result.then(result => {
      alert(result);
    });
  }

  public onViewTreatment() {
    alert('Clicked');
  }
}
