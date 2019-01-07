import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { map, startWith, debounceTime, switchMap } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from '../../../shared/modules/modal/modal-component';
import { Constants } from '../../../shared/constants/constants';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AddressService } from '../../../shared/services/address.service';
import { CountryModel } from '../../../shared/models/address/country.model';
import { ProvinceModel } from '../../../shared/models/address/province.model';
import { DistrictModel } from '../../../shared/models/address/district.model';
import { PatientService } from '../../patients/shared/patient.service';
import { PatientModel } from '../../patients/shared/patient.model';
import { Subject, Observable } from 'rxjs';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { MedicalRecordModel } from '../shared/medical-record.model';
import { MedicalRecordService } from '../shared/medical-record.service';

@Component({
  selector: 'app-medical-record-add',
  templateUrl: './medical-record-add.component.html',
  styleUrls: ['./medical-record-add.component.scss']
})
export class MedicalRecordAddComponent implements OnInit {
  myForm: FormGroup;
  submitted = false;
  medicalRecordControl = 'Thêm mới bệnh án';
  countryControl: FormControl;
  provinceControl: FormControl;
  districtControl: FormControl;
  nativeCountryControl: FormControl;
  nativeProvinceControl: FormControl;
  nativeDistrictControl: FormControl;
  patientControl: FormControl;
  startDateControl: FormControl;
  reasonControl: FormControl;
  statusIdControl: FormControl;
  filteredPatient: Observable<PatientModel[]>;
  patients: PatientModel[];
  patientRouteId: number;
  public inputData: MedicalRecordModel;
  isLoadingPatient = false;
  private nextPage$ = new Subject();

  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private patientService: PatientService,
    private medicalRecordService: MedicalRecordService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router) {

    this.inputData = new MedicalRecordModel();
    this.patientControl = new FormControl();
    this.startDateControl = new FormControl();
    this.reasonControl = new FormControl();
    this.statusIdControl = new FormControl();
  }

  ngOnInit() {
    if (this.route.params) {
      this.patientRouteId = this.route.params['value'].id;
    }
    this.dropdownConfig(() => this.spinner.hide());
    this.myForm = this.formBuilder.group({
      patientControl: [0, Validators.required],
      startDateControl: ['', Validators.required],
      reasonControl: ['', Validators.required],
      statusIdControl: [0, Validators.required],
    });
  }

  private dropdownConfig(callback) {
    this.getPatients('')
    .subscribe((data: PaginatedListModel<PatientModel>) => {
      this.patients = data.items;
      this.filteredPatient = this.patientControl.valueChanges.pipe(
        startWith(''),
        map(value => this.filterPatient(value)));
    });
    callback();
  }

  private filterPatient(value: string) {
    const valueFilter = (value || '').toString();
    return this.patients.filter(c => this.checkMatchStringFilter(c.fullName, valueFilter));
  }

  public getPatients = (value: string): Observable<PaginatedListModel<PatientModel>> => {
    const params = {
      pageIndex: 0,
      pageSize: 2000,
      code: 'name',
      value: typeof(value) === 'string' && value ? value.trim() : '',
    };
    return this.patientService.get(params);
  }

  public onSubmit(): void {

    if (this.myForm.invalid) {
      this.submitted = true;
      return;
    }

    this.onAdd();
  }

  private checkMatchStringFilter = (source: string, searchString: string): boolean => {
    return this.removeUnicode(source.toLowerCase()).indexOf(this.removeUnicode(searchString.toLowerCase())) >= 0;
  }

  private removeUnicode(value: string) {
    value = value.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a');
    value = value.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e');
    value = value.replace(/ì|í|ị|ỉ|ĩ/g, 'i');
    value = value.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o');
    value = value.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u');
    value = value.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y');
    value = value.replace(/đ/g, 'd');
    value = value.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, 'A');
    value = value.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, 'E');
    value = value.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, 'I');
    value = value.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, 'O');
    value = value.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, 'U');
    value = value.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, 'Y');
    value = value.replace(/Đ/g, 'D');
    return value;
  }

  private onAdd(): void {
    this.medicalRecordService.add(this.inputData).subscribe(
      (data: number) => {
        this.spinner.hide();
        if (data > 0) {
          const modalRef = this.modal.open(NgbdModalComponent);
          modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef.componentInstance.content = Constants.MODAL.ADD_SUCCESS;
          modalRef.componentInstance.isDisplayCancel = false;
          modalRef.result.then(_result => {
            this.router.navigate(['/medical-records']);
          });
        } else {
          const modalRef = this.modal.open(NgbdModalComponent);
          modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef.componentInstance.content = Constants.MODAL.ADD_FAIL;
          modalRef.componentInstance.isDisplayCancel = false;
        }
      },
      (err) => {
        this.spinner.hide();
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.ADD_FAIL;
        modalRef.componentInstance.isDisplayCancel = false;
      },
      () => {
        // this.spinner.hide();
        // const modalRef = this.modal.open(NgbdModalComponent);
        // modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        // modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
        // modalRef.componentInstance.isDisplayCancel = false;
      }
    );
  }
}
