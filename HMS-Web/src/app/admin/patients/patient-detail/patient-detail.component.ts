import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource, PageEvent } from '@angular/material';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { faBriefcase } from '@fortawesome/fontawesome-free-solid';
import { NgxSpinnerService } from 'ngx-spinner';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientService } from '../shared/patient.service';
import { AddressService } from '../../../shared/services/address.service';
import { FilterModel } from '../../../shared/models/filter.model';
import { PatientModel } from '../shared/patient.model';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { NgbdModalComponent } from '../../../shared/modules/modal/modal-component';
import { Constants } from '../../../shared/constants/constants';
import { CountryModel } from '../../../shared/models/address/country.model';
import { ProvinceModel } from '../../../shared/models/address/province.model';
import { DistrictModel } from '../../../shared/models/address/district.model';
import { MedicalRecordService } from '../../medical-records/shared/medical-record.service';
import { MedicalRecordModel } from '../../medical-records/shared/medical-record.model';
import { TreatmentModel } from '../../treatments/shared/treatment.model';
import { TreatmentService } from '../../treatments/shared/treatment.service';
import { TreatmentDetailModel } from '../../treatment-details/shared/treatment-detail.model';
import { TreatmentDetailService } from '../../treatment-details/shared/treatment-detail.service';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.scss']
})
export class PatientDetailComponent implements OnInit {
  myForm: FormGroup;
  patientRouteId: number;
  countryControl: FormControl;
  provinceControl: FormControl;
  districtControl: FormControl;
  nativeCountryControl: FormControl;
  nativeProvinceControl: FormControl;
  nativeDistrictControl: FormControl;
  filteredCountries: any;
  filteredProvinces: any;
  filteredDistricts: any;
  filteredNativeCountries: any;
  filteredNativeProvinces: any;
  filteredNativeDistricts: any;
  public inputData: PatientModel;
  countries: CountryModel[];
  provinces: ProvinceModel[];
  districts: DistrictModel[];
  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private formBuilder: FormBuilder,
    private patientService: PatientService,
    private addressService: AddressService,
    private treatmentService: TreatmentService,
    private treatmentDetailService: TreatmentDetailService,
    private route: ActivatedRoute,
    private medicalRecordService: MedicalRecordService,
    private router: Router) {

    this.inputData = new PatientModel();
    this.inputData.sex = 1;
    this.countryControl = new FormControl();
    this.provinceControl = new FormControl();
    this.districtControl = new FormControl();
    this.nativeCountryControl = new FormControl();
    this.nativeProvinceControl = new FormControl();
    this.nativeDistrictControl = new FormControl();
  }

  // icon
  public faBriefcase = faBriefcase;
  pageEvent: PageEvent;
  // Search AutoComplete
  public searchControl = new FormControl();
  public filteredOptions: Observable<string[]>;
  @ViewChild(MatPaginator) medicalRecordPaginator: MatPaginator;
  @ViewChild(MatPaginator) treatmentPaginator: MatPaginator;
  @ViewChild(MatPaginator) treatmentDetailPaginator: MatPaginator;
  public options: string[] = [];
  public filter: FilterModel = new FilterModel();

  // Data list [Start]
  // tslint:disable-next-line:member-ordering
  public medicalRecordDisplayedColumns: string[] = [
    'Code',
    'StartDate',
    'EndDate',
    'Reason',
    'Diagnose',
    'StatusName',
    'control'
  ];
  public treatmentDisplayedColumns: string[] = [
    'Code',
    'MedicalRecordCode',
    'StartDate',
    'Title',
    'Content',
    'Doctor',
    'Nurse',
    'Note',
    'control'
  ];
  public treatmentDetailDisplayedColumns: string[] = [
    'Code',
    'TreatmentCode',
    'MedicalRecordCode',
    'StartDate',
    'Content',
    'Result',
    'Doctor',
    'Nurse',
    'Note',
    'control'
  ];
  public medicalRecordDataSource = new MatTableDataSource<MedicalRecordModel>();
  public treatmentDataSource = new MatTableDataSource<TreatmentModel>();
  public treatmentDetailDataSource = new MatTableDataSource<TreatmentDetailModel>();

  public ngOnInit() {
    // tslint:disable-next-line:radix
    this.patientRouteId = parseInt(this.route.params['value'].id);
    this.initAddress();
    this.getDetail(this.patientRouteId);

    this.medicalRecordPaginator.pageIndex = 0;
    this.medicalRecordPaginator.pageSize = 10;
    this.treatmentPaginator.pageIndex = 0;
    this.treatmentPaginator.pageSize = 10;
    this.treatmentDetailPaginator.pageIndex = 0;
    this.treatmentDetailPaginator.pageSize = 10;
    // this.dataSource.paginator = this.paginator;
    this.filter.code = 'code';
    this.filter.value = '';
    this.searchConfig();
    this.myForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      sex: ['', Validators.required],
      phone: ['', Validators.required],
      birthday: ['', Validators.required],
      address: ['', Validators.required],
      countryControl: [0, Validators.required],
      provinceControl: [0, Validators.required],
      districtControl: [0, Validators.required],
      nativeAddress: ['', Validators.required],
      nativeCountryControl: [0, Validators.required],
      nativeProvinceControl: [0, Validators.required],
      nativeDistrictControl: [0, Validators.required],
    });
  }

  private getDetail(id: number) {
    this.patientService.getDetail(id).subscribe(
      (data: PatientModel) => {
        this.inputData = data;
      },
      (error: any) => {
        console.log(error);
      },
      () => {
        this.spinner.hide();
      }
    );
  }

  private initAddress() {
    this.addressService.init();
    this.countries = this.addressService.countries;
    this.filteredCountries = this.countryControl.valueChanges.pipe(
      startWith(null),
      map(value => this.filterCountry(value))
    );
    this.filteredNativeCountries = this.nativeCountryControl.valueChanges.pipe(
      startWith(null),
      map(value => this.filterCountry(value))
    );
    this.provinces = this.addressService.provinces;
    this.filteredProvinces = this.provinceControl.valueChanges.pipe(
      startWith(null),
      map(value => this.filterProvince(value, this.inputData.countryId))
    );
    this.filteredNativeProvinces = this.nativeProvinceControl.valueChanges.pipe(
      startWith(null),
      map(value => this.filterProvince(value, this.inputData.nativeCountryId))
    );
    this.districts = this.addressService.districts;
    this.filteredDistricts = this.districtControl.valueChanges.pipe(
      startWith(null),
      map(value => this.filterDistrict(value, this.inputData.provinceId))
    );
    this.filteredNativeDistricts = this.nativeDistrictControl.valueChanges.pipe(
      startWith(null),
      map(value => this.filterDistrict(value, this.inputData.nativeProvinceId))
    );
  }

  public displayCountry = (countryId: number): string => {
    const country = this.countries ? this.countries.find(c => c.id === countryId) : null;
    return country ? country.name : '';
  }

  public displayProvince = (provinceId: number): string => {
    const province = this.provinces ? this.provinces.find(c => c.id === provinceId) : null;
    return province ? province.name : '';
  }

  public displayDistrict = (districtId: number): string => {
    const district = this.districts ? this.districts.find(c => c.id === districtId) : null;
    return district ? district.name : '';
  }

  private filterCountry(value: string) {
    const valueFilter = (value || '').toString();
    return this.countries.filter(c => this.checkMatchStringFilter(c.name, valueFilter));
  }

  private filterProvince(value: string, countryId: number) {
    const valueFilter = (value || '').toString();
    return this.provinces.filter(c => c.countryId === countryId && this.checkMatchStringFilter(c.name, valueFilter));
  }

  private filterDistrict(value: string, provinceId: number) {
    const valueFilter = (value || '').toString();
    return this.districts.filter(c => c.provinceId === provinceId && this.checkMatchStringFilter(c.name, valueFilter));
  }
  private checkMatchStringFilter = (source: string, searchString: string): boolean => {
    return this.removeUnicode(source.toLowerCase()).indexOf(this.removeUnicode(searchString.toLowerCase())) === 0;
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

  public searchConfig() {
    this.spinner.show();
    this.searchConfigMedicalRecord();
    this.searchConfigTreatment();
    this.searchConfigTreatmentDetail();
  }

  public searchConfigMedicalRecord() {
    this.spinner.show();
    const medicalRecordParams = {
      pageIndex: this.medicalRecordPaginator.pageIndex,
      pageSize: this.medicalRecordPaginator.pageSize,
      patientId: this.patientRouteId
    };
    this.medicalRecordService.getByPatientId(medicalRecordParams).subscribe(
      this.onGetNextMedicalRecord,
      this.onGetErrorMedicalRecord,
      this.onGetCompleteMedicalRecord
    );
  }

  public searchConfigTreatment() {
    this.spinner.show();
    const treatmentParams = {
      pageIndex: this.treatmentPaginator.pageIndex,
      pageSize: this.treatmentPaginator.pageSize,
      patientId: this.patientRouteId
    };
    this.treatmentService.getByPatientId(treatmentParams).subscribe(
      this.onGetNextTreatment,
      this.onGetErrorTreatment,
      this.onGetCompleteTreatment
    );
  }
  public searchConfigTreatmentDetail() {
    this.spinner.show();
    const params = {
      pageIndex: this.treatmentDetailPaginator.pageIndex,
      pageSize: this.treatmentDetailPaginator.pageSize,
      patientId: this.patientRouteId
    };
    this.treatmentDetailService.getByPatientId(params).subscribe(
      this.onGetNextTreatmentDetail,
      this.onGetErrorTreatmentDetail,
      this.onGetCompleteTreatmentDetail
    );
  }

  private onGetErrorTreatmentDetail = (err) => {
    console.log(err);
    this.spinner.hide();
  }

  private onGetCompleteTreatmentDetail = () => {
    this.spinner.hide();
  }

  private onGetNextTreatmentDetail = (data: PaginatedListModel<TreatmentDetailModel>) => {
    this.treatmentDetailDataSource.data = data.items;
    this.treatmentDetailPaginator.length = data.totalItems;
  }


  private onGetErrorTreatment = (err) => {
    console.log(err);
    this.spinner.hide();
  }

  private onGetCompleteTreatment = () => {
    this.spinner.hide();
  }

  private onGetNextTreatment = (data: PaginatedListModel<TreatmentModel>) => {
    this.treatmentDataSource.data = data.items;
    this.treatmentPaginator.length = data.totalItems;
  }

  private onGetErrorMedicalRecord = (err) => {
    console.log(err);
    this.spinner.hide();
  }

  private onGetCompleteMedicalRecord = () => {
    this.spinner.hide();
  }

  private onGetNextMedicalRecord = (data: PaginatedListModel<MedicalRecordModel>) => {
    this.medicalRecordDataSource.data = data.items;
    this.medicalRecordPaginator.length = data.totalItems;
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option =>
      option.toLowerCase().includes(filterValue)
    );
  }

  public onPageChangeMedicalRecord(event: PageEvent) {
    this.pageEvent = event;
    this.searchConfigMedicalRecord();
  }

  public onPageChangeTreatment(event: PageEvent) {
    this.pageEvent = event;
    this.searchConfigTreatment();
  }

  // Data list [End]

  public onUpdate() {
    alert('Clicked');
  }

  public onDisable(patientId: number) {
    const modalRef = this.modal.open(NgbdModalComponent);
    modalRef.componentInstance.header = Constants.MODAL.DISABLE_HEADER;
    modalRef.componentInstance.content = Constants.MODAL.DISABLE_PATIENT_CONTENT;
    modalRef.componentInstance.isDisplayCancel = true;
    modalRef.result.then(_result => {
      const params = {
        id: patientId,
        isActive: false
      };
      this.patientService.active(params).subscribe(
        (data: boolean) => {
          this.spinner.hide();
          if (data) {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
            modalRef2.componentInstance.isDisplayCancel = false;
            modalRef2.result.then(result => {
              this.searchConfig();
            });
          } else {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
            modalRef2.componentInstance.isDisplayCancel = false;
          }
        },
        (_err) => {
          this.spinner.hide();
          const modalRef2 = this.modal.open(NgbdModalComponent);
          modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          modalRef2.componentInstance.isDisplayCancel = false;
        },
        () => {
          // this.spinner.hide();
          // const modalRef = this.modal.open(NgbdModalComponent);
          // modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          // modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          // modalRef.componentInstance.isDisplayCancel = false;
        });
    })
      .catch(res => {
        console.log(res);
      });
  }

  public onEnable(patientId: number) {
    const modalRef = this.modal.open(NgbdModalComponent);
    modalRef.componentInstance.header = Constants.MODAL.ENABLE_HEADER;
    modalRef.componentInstance.content = Constants.MODAL.ENABLE_PATIENT_CONTENT;
    modalRef.componentInstance.isDisplayCancel = true;
    modalRef.result.then(_result => {
      const params = {
        id: patientId,
        isActive: true
      };
      this.patientService.active(params).subscribe(
        (data: boolean) => {
          this.spinner.hide();
          if (data) {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
            modalRef2.componentInstance.isDisplayCancel = false;
            modalRef2.result.then(result => {
              this.searchConfig();
            });
          } else {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
            modalRef2.componentInstance.isDisplayCancel = false;
          }
        },
        (_err) => {
          this.spinner.hide();
          const modalRef2 = this.modal.open(NgbdModalComponent);
          modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          modalRef2.componentInstance.isDisplayCancel = false;
        },
        () => {
          // this.spinner.hide();
          // const modalRef = this.modal.open(NgbdModalComponent);
          // modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          // modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          // modalRef.componentInstance.isDisplayCancel = false;
        });
    })
      .catch(res => {
        console.log(res);
      });
  }

  public onDisableMedicalRecord(patientId: number) {
    const modalRef = this.modal.open(NgbdModalComponent);
    modalRef.componentInstance.header = Constants.MODAL.DISABLE_HEADER;
    modalRef.componentInstance.content = Constants.MODAL.DISABLE_MEDICALRECORD_CONTENT;
    modalRef.componentInstance.isDisplayCancel = true;
    modalRef.result.then(result => {
      const params = {
        id: patientId,
        isActive: false
      };
      this.medicalRecordService.active(params).subscribe(
        (data: boolean) => {
          this.spinner.hide();
          if (data) {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
            modalRef2.componentInstance.isDisplayCancel = false;
            modalRef2.result.then(_result => {
              this.searchConfig();
            });
          } else {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
            modalRef2.componentInstance.isDisplayCancel = false;
          }
        },
        (err) => {
          this.spinner.hide();
          const modalRef2 = this.modal.open(NgbdModalComponent);
          modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          modalRef2.componentInstance.isDisplayCancel = false;
        },
        () => {
          // this.spinner.hide();
          // const modalRef = this.modal.open(NgbdModalComponent);
          // modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          // modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          // modalRef.componentInstance.isDisplayCancel = false;
        });
    })
      .catch(res => {
        console.log(res);
      });
  }

  public onEnableMedicalRecord(patientId: number) {
    const modalRef = this.modal.open(NgbdModalComponent);
    modalRef.componentInstance.header = Constants.MODAL.ENABLE_HEADER;
    modalRef.componentInstance.content = Constants.MODAL.ENABLE_MEDICALRECORD_CONTENT;
    modalRef.componentInstance.isDisplayCancel = true;
    modalRef.result.then(result => {
      const params = {
        id: patientId,
        isActive: true
      };
      this.medicalRecordService.active(params).subscribe(
        (data: boolean) => {
          this.spinner.hide();
          if (data) {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
            modalRef2.componentInstance.isDisplayCancel = false;
            modalRef2.result.then(_result => {
              this.searchConfig();
            });
          } else {
            const modalRef2 = this.modal.open(NgbdModalComponent);
            modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
            modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
            modalRef2.componentInstance.isDisplayCancel = false;
          }
        },
        (err) => {
          this.spinner.hide();
          const modalRef2 = this.modal.open(NgbdModalComponent);
          modalRef2.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef2.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          modalRef2.componentInstance.isDisplayCancel = false;
        },
        () => {
          // this.spinner.hide();
          // const modalRef = this.modal.open(NgbdModalComponent);
          // modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          // modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          // modalRef.componentInstance.isDisplayCancel = false;
        });
    })
      .catch(res => {
        console.log(res);
      });
  }

  public onViewTreatment() {
    alert('Clicked');
  }
}
