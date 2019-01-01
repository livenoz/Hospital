import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from '../../../shared/modules/modal/modal-component';
import { Constants } from '../../../shared/constants/constants';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { AddressService } from '../../../shared/services/address.service';
import { CountryModel } from '../../../shared/models/address/country.model';
import { ProvinceModel } from '../../../shared/models/address/province.model';
import { DistrictModel } from '../../../shared/models/address/district.model';
import { PatientModel } from '../../patients/shared/patient.model';
import { PatientService } from '../../patients/shared/patient.service';

@Component({
  selector: 'app-patient-update',
  templateUrl: './patient-update.component.html',
  styleUrls: ['./patient-update.component.scss']
})
export class PatientUpdateComponent implements OnInit {
  myForm: FormGroup;
  submitted = false;
  patientControl = 'Cập nhật bệnh nhân';
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
  patientRouteId: number;
  public inputData: PatientModel;
  countries: CountryModel[];
  provinces: ProvinceModel[];
  districts: DistrictModel[];

  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private patientService: PatientService,
    private addressService: AddressService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
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

  ngOnInit() {
    if (this.route.params) {
      this.patientRouteId = this.route.params['value'].id;
    }
    this.dropdownConfig(() => this.spinner.hide());
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

  private dropdownConfig(callback) {
    this.initAddress();
    this.getDetail(this.patientRouteId);
    callback();
  }

  private initAddress() {
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
      return;
    }

    this.onUpdate();
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

  private onUpdate(): void {
    this.patientService.update(this.inputData).subscribe(
      (data: boolean) => {
        this.spinner.hide();
        if (data) {
          const modalRef = this.modal.open(NgbdModalComponent);
          modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
          modalRef.componentInstance.isDisplayCancel = false;
          modalRef.result.then(_result => {
            this.router.navigate(['/patients']);
          });
        } else {
          const modalRef = this.modal.open(NgbdModalComponent);
          modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          modalRef.componentInstance.isDisplayCancel = false;
        }
      },
      (err) => {
        this.spinner.hide();
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
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
