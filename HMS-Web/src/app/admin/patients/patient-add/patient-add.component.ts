import * as core from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { PatientService } from '../shared/patient.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from './../../../shared/modules/modal/modal-component';
import { Constants } from './../../../shared/constants/constants';
import { ActivatedRoute } from '@angular/router';
import { PatientModel } from '../shared/patient.model';

@core.Component({
  selector: 'app-patient-add',
  templateUrl: './patient-add.component.html',
  styleUrls: ['./patient-add.component.scss']
})
export class PatientAddComponent implements core.OnInit {
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
  countries = [
    'Việt Nam'
  ];
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
      this.filteredCountries = this.countryControl.valueChanges.pipe(
        startWith(null),
        map(name => this._filter(name, this.countries)));
      this.filterProvinces = this.provinceControl.valueChanges.pipe(
        startWith(null),
        map(name => this._filter(name, this.provinces)));
      this.filterDistrictes = this.districtControl.valueChanges.pipe(
        startWith(null),
        map(name => this._filter(name, this.districtes)));
      callback();
    }, 0);
  }

  // Search AutoComplete
  private _filter(value: string, options): string[] {
    return value ? options.filter(s => s.toLowerCase().indexOf(value.toLowerCase()) === 0)
      : options;
  }

  public onSubmit(): void {
    if (this.myForm.invalid) {
      this.submitted = true;
      return;
    }

    this.onAdd();
  }

  public onAdd(): void {
    this.patientService.add(this.inputData).subscribe(
      () => {
        const modalRef = this.modal.open(NgbdModalComponent);
        modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
        modalRef.componentInstance.content = Constants.MODAL.ADD_SUCCESS;
        modalRef.componentInstance.isDisplayCancel = false;
        modalRef.result.then(_result => {
          this.spinner.hide();
          location.href = '/patient-profiles';
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
