import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, MatTableDataSource, PageEvent } from "@angular/material";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { faBriefcase } from "@fortawesome/fontawesome-free-solid";
import { NgxSpinnerService } from "ngx-spinner";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { NgbdModalComponent } from './../../shared/modules/modal/modal-component';
import { Constants } from './../../shared/constants/Constants';
import { PatientService } from './../../shared/services/patient.service'
import { PatientProfileModel } from './patient-profiles-model'

@Component({
  selector: "app-patient-profiles",
  templateUrl: "./patient-profiles.component.html",
  styleUrls: ["./patient-profiles.component.scss"]
})
export class PatientProfilesComponent implements OnInit {
  constructor(private spinner: NgxSpinnerService, private modal: NgbModal, private patientService: PatientService) {
    
  }

  // icon
  faBriefcase = faBriefcase;

  patientGroups: PatientProfileModel[] = [];
  // Search AutoComplete
  searchControl = new FormControl();
  filteredOptions: Observable<string[]>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  options: string[] = [];

  ngOnInit() {
    this.searchConfig();
  }

  searchConfig() {
    setTimeout(() => {
      this.filteredOptions = this.searchControl.valueChanges.pipe(
        startWith(""),
        map(value => this._filter(value))
      );
      this.dataSource.paginator = this.paginator;
      let params = {
        pageIndex: this.paginator.pageIndex, 
        pageSize: this.paginator.pageSize
      };
      this.patientService.get(params).subscribe(
        (data: any) => {
          this.patientGroups = data;
          this.options = this.patientGroups.map(x => x.fullName);
        },
        (err) => this.spinner.hide(),
        // The 3rd callback handles the "complete" event.
        () => this.spinner.hide()
      );
    }, 0);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option =>
      option.toLowerCase().includes(filterValue)
    );
  }

  public onPageChange(event?: PageEvent){
    this.spinner.show();
    let params = {
      pageIndex: this.paginator.pageIndex, 
      pageSize: this.paginator.pageSize
    };
    this.patientService.get(params).subscribe(
      (data: any) => {
        this.patientGroups = data;
        this.options = this.patientGroups.map(x => x.fullName);
      },
      (err) => this.spinner.hide(),
      // The 3rd callback handles the "complete" event.
      () => this.spinner.hide()
    );
  }

  // Data list [Start]
  displayedColumns: string[] = [
    "Code",
    "FullName",
    "IdentifyCardNo",
    "Phone",
    "Address",
    "Sex",
    "control"
  ];
  dataSource = new MatTableDataSource<PatientProfileModel>(this.patientGroups);

  // Data list [End]

  onUpdate() {
    alert("Clicked");
  }

  onDisable() {
    const modalRef = this.modal.open(NgbdModalComponent);
    modalRef.componentInstance.header = Constants.MODAL.DISABLE_HEADER;
    modalRef.componentInstance.content = Constants.MODAL.DISABLE_CONTENT;
    modalRef.result.then(result => {
      alert(result);
    });
  }

  onViewTreatment() {
    alert("Clicked");
  }
}
