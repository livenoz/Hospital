import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { PatientService } from '../shared/patient.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from '../../../shared/modules/modal/modal-component';
import { Constants } from '../../../shared/constants/constants';
import { ActivatedRoute } from '@angular/router';
import { PatientModel } from '../shared/patient.model';
import { AddressService } from '../../../shared/services/address.service';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { CountryModel } from '../../../shared/models/address/country.model';

@Component({
  selector: 'app-patient-update',
  templateUrl: './patient-update.component.html',
  styleUrls: ['./patient-update.component.scss']
})
export class PatientUpdateComponent implements OnInit {
  myForm: FormGroup;
  submitted = false;
  patientControl = 'Thêm mới bệnh nhân';
  countryControl: FormControl;
  provinceControl: FormControl;
  districtControl: FormControl;
  filteredCountries: any;
  filterProvinces: any;
  filterDistrictes: any;
  patientRouteId: number;
  public inputData: PatientModel;
  countries: CountryModel[];
  provinces = [
    'Hồ Chí Minh',
    'Đà Nẵng',
    'Hà Nội'
  ];
  districtes = [
    '1',
    '2',
    '3',
    '4',
    '5',
    '6'
  ];

  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private patientService: PatientService,
    private addressService: AddressService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute) {

    this.inputData = new PatientModel();
    this.inputData.sex = 1;
    this.countryControl = new FormControl();
    this.provinceControl = new FormControl();
    this.districtControl = new FormControl();
  }

  ngOnInit() {
    this.dropdownConfig(() => this.spinner.hide());
    if (this.route.params) {
      this.patientRouteId = this.route.params['value'].id;
    }
    this.myForm = this.formBuilder.group({
      code: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      sex: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', Validators.required],
      selectedDate: ['', Validators.required],
      identifyCardNo: ['', Validators.required],
      dateOfIssue: ['', Validators.required],
      placeOfIssue: ['', Validators.required],
      address: ['', Validators.required],
      countryId: ['', Validators.required],
      provinceId: ['', Validators.required],
      districtId: ['', Validators.required],
      nativeAddress: ['', Validators.required],
      nativeCountryId: ['', Validators.required],
      nativeProvinceId: ['', Validators.required],
      nativeDistrictId: ['', Validators.required],
      fatherName: ['', Validators.required],
      fatherIdentifyCardNo: ['', Validators.required],
      motherName: ['', Validators.required],
      motherIdentifyCardNo: ['', Validators.required],
    });
  }

  dropdownConfig(callback) {
    setTimeout(() => {
      this.getAllCountries();
      // this.filterProvinces = this.provinceControl.valueChanges.pipe(
      //   startWith(null),
      //   map(name => this.filter(name, this.provinces)));
      // this.filterDistrictes = this.districtControl.valueChanges.pipe(
      //   startWith(null),
      //   map(name => this.filter(name, this.districtes)));
      this.getDetail(this.patientRouteId);
      callback();
    }, 0);
  }

  public displayFn = (countryId: number): string => {
    const country = this.countries ? this.countries.find(c => c.id === countryId) : null;
    return country ? country.name : '';
  }

  private getAllCountries() {
    this.addressService.getAllCountries().subscribe((data: PaginatedListModel<CountryModel>) => {
      this.countries = data.items;
      this.filteredCountries = this.countryControl.valueChanges.pipe(
        startWith(null),
        map(value => this.filterCountry(value))
      );
    });
  }

  private filterCountry(value: string): CountryModel[] {
    const valueFilter = (value || '').toString();
    return this.countries.filter(c => c.name.toLowerCase().indexOf(valueFilter.toLowerCase()) === 0);
  }

  // Search AutoComplete
  private filter(value: string, options): string[] {
    return value ? options.filter(s => s.toLowerCase().indexOf(value.toLowerCase()) === 0)
      : options;
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

  public onSubmit(): void {

    if (this.myForm.invalid) {
      this.submitted = true;
      return;
    }

    if (this.patientRouteId) {
      this.onUpdate();
    } else {
      this.onAdd();
    }
  }

  public onUpdate(): void {
    this.patientService.update(this.patientRouteId).subscribe(
      (_data: any) => {
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
        modalRef.componentInstance.isDisplayCancel = false;
        modalRef.result.then(_result => {
          this.spinner.hide();
          location.href = '/patients';
        });
      },
      (_err) => {
        this.spinner.hide();
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
        modalRef.componentInstance.isDisplayCancel = false;
      },
      () => {
        this.spinner.hide();
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
        modalRef.componentInstance.isDisplayCancel = false;
      }
    );
  }

  public onAdd(): void {
    this.patientService.add(this.inputData).subscribe(
      (_data: any) => {
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.ADD_SUCCESS;
        modalRef.componentInstance.isDisplayCancel = false;
        modalRef.result.then(_result => {
          this.spinner.hide();
          location.href = '/patients';
        });
      },
      (_err) => {
        this.spinner.hide();
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.ADD_FAIL;
        modalRef.componentInstance.isDisplayCancel = false;
      },
      () => {
        this.spinner.hide();
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.ADD_FAIL;
        modalRef.componentInstance.isDisplayCancel = false;
      }
    );
  }
}
