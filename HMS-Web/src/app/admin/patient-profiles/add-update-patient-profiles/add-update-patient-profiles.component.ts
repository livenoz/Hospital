import { Component, OnInit, ViewChild } from "@angular/core";
import { NgxSpinnerService } from "ngx-spinner";
import { FormControl } from "@angular/forms";
import { map, startWith } from "rxjs/operators";
import { AddUpdatePatient } from "./add-update-patient.model"

@Component({
  selector: "app-add-update-patient-profiles",
  templateUrl: "./add-update-patient-profiles.component.html",
  styleUrls: ["./add-update-patient-profiles.component.scss"]
})
export class AddUpdatePatientProfilesComponent implements OnInit {
  birthday;
  patientControl = "Thêm mới bệnh nhân";
  countryControl: FormControl;
  provinceControl: FormControl;
  districtControl: FormControl;
  filteredCountries: any;
  filterProvinces: any;
  filterDistrictes: any;
  public inputData: AddUpdatePatient;
  countries = [
    'Việt Nam'
  ]
  provinces = [
    'Hồ Chí Minh',
    'Đà Nẵng',
    'Hà Nội'
  ]
  districtes = [
    '1',
    '2',
    '3',
    '4',
    '5',
    '6'
  ]

  constructor(private spinner: NgxSpinnerService) {
    this.inputData = new AddUpdatePatient();
    this.countryControl = new FormControl();
    this.provinceControl = new FormControl();
    this.districtControl = new FormControl();
  }

  ngOnInit() {
    this.dropdownConfig(() => this.spinner.hide());
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

  private _filter(value: string, options): string[] {
    return value ? options.filter(s => s.toLowerCase().indexOf(value.toLowerCase()) === 0)
      : options;
  }
  // Search AutoComplete [End]
}
