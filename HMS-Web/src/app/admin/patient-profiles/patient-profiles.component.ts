import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, MatTableDataSource } from "@angular/material";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs";
import { map, startWith } from "rxjs/operators";
import { faBriefcase } from "@fortawesome/fontawesome-free-solid";
import { NgxSpinnerService } from "ngx-spinner";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { NgbdModalComponent } from './../../shared/modules/modal/modal-component';

export interface PatientElement {
  id: number;
  patientCode: string;
  name: string;
  phoneNumber: string;
  ssn: string;
}

@Component({
  selector: "app-patient-profiles",
  templateUrl: "./patient-profiles.component.html",
  styleUrls: ["./patient-profiles.component.scss"]
})
export class PatientProfilesComponent implements OnInit {
  constructor(private spinner: NgxSpinnerService, private modal: NgbModal) {}

  // icon
  faBriefcase = faBriefcase;

  patientGroups: PatientElement[] = [
    {
      id: 1,
      patientCode: "HSM00001",
      name: "Manh",
      phoneNumber: "01239123707",
      ssn: "0222733444"
    },
    {
      id: 2,
      patientCode: "HSM00002",
      name: "Hoa",
      phoneNumber: "01239123708",
      ssn: "0222383444"
    },
    {
      id: 3,
      patientCode: "HSM00003",
      name: "Thanh",
      phoneNumber: "01239123709",
      ssn: "0229333444"
    },
    {
      id: 4,
      patientCode: "HSM00004",
      name: "Hien",
      phoneNumber: "01239123710",
      ssn: "0222033444"
    },
    {
      id: 5,
      patientCode: "HSM00005",
      name: "Duy",
      phoneNumber: "01239123719",
      ssn: "02987210394"
    },
    {
      id: 6,
      patientCode: "HSM00006",
      name: "Toan",
      phoneNumber: "01239123711",
      ssn: "0222133444"
    },
    {
      id: 7,
      patientCode: "HSM00007",
      name: "Quy",
      phoneNumber: "01239123712",
      ssn: "0222323444"
    },
    {
      id: 8,
      patientCode: "HSM00008",
      name: "Uyen",
      phoneNumber: "01239123706",
      ssn: "022333444"
    },
    {
      id: 9,
      patientCode: "HSM00009",
      name: "Hau",
      phoneNumber: "01239123705",
      ssn: "0222433444"
    },
    {
      id: 10,
      patientCode: "HSM00010",
      name: "Hong",
      phoneNumber: "01239123704",
      ssn: "0225333444"
    },
    {
      id: 11,
      patientCode: "HSM00011",
      name: "Tam",
      phoneNumber: "01239123703",
      ssn: "0222633444"
    },
    {
      id: 6,
      patientCode: "HSM00006",
      name: "Toan",
      phoneNumber: "01239123711",
      ssn: "0222133444"
    },
    {
      id: 7,
      patientCode: "HSM00007",
      name: "Quy",
      phoneNumber: "01239123712",
      ssn: "0222323444"
    },
    {
      id: 8,
      patientCode: "HSM00008",
      name: "Uyen",
      phoneNumber: "01239123706",
      ssn: "022333444"
    },
    {
      id: 9,
      patientCode: "HSM00009",
      name: "Hau",
      phoneNumber: "01239123705",
      ssn: "0222433444"
    },
    {
      id: 10,
      patientCode: "HSM00010",
      name: "Hong",
      phoneNumber: "01239123704",
      ssn: "0225333444"
    },
    {
      id: 11,
      patientCode: "HSM00011",
      name: "Tam",
      phoneNumber: "01239123703",
      ssn: "0222633444"
    },
    {
      id: 7,
      patientCode: "HSM00007",
      name: "Quy",
      phoneNumber: "01239123712",
      ssn: "0222323444"
    },
    {
      id: 8,
      patientCode: "HSM00008",
      name: "Uyen",
      phoneNumber: "01239123706",
      ssn: "022333444"
    },
    {
      id: 9,
      patientCode: "HSM00009",
      name: "Hau",
      phoneNumber: "01239123705",
      ssn: "0222433444"
    },
    {
      id: 10,
      patientCode: "HSM00010",
      name: "Hong",
      phoneNumber: "01239123704",
      ssn: "0225333444"
    },
    {
      id: 11,
      patientCode: "HSM00011",
      name: "Tam",
      phoneNumber: "01239123703",
      ssn: "0222633444"
    },
    {
      id: 6,
      patientCode: "HSM00006",
      name: "Toan",
      phoneNumber: "01239123711",
      ssn: "0222133444"
    },
    {
      id: 7,
      patientCode: "HSM00007",
      name: "Quy",
      phoneNumber: "01239123712",
      ssn: "0222323444"
    },
    {
      id: 8,
      patientCode: "HSM00008",
      name: "Uyen",
      phoneNumber: "01239123706",
      ssn: "022333444"
    },
    {
      id: 9,
      patientCode: "HSM00009",
      name: "Hau",
      phoneNumber: "01239123705",
      ssn: "0222433444"
    },
    {
      id: 10,
      patientCode: "HSM00010",
      name: "Hong",
      phoneNumber: "01239123704",
      ssn: "0225333444"
    },
    {
      id: 11,
      patientCode: "HSM00011",
      name: "Tam",
      phoneNumber: "01239123703",
      ssn: "0222633444"
    }
  ];

  // Search AutoComplete [Start]
  searchControl = new FormControl();
  options: string[] = this.patientGroups.map(x => x.name);
  filteredOptions: Observable<string[]>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  ngOnInit() {
    this.searchConfig(() => this.spinner.hide());
  }

  searchConfig(callback) {
    setTimeout(() => {
      this.filteredOptions = this.searchControl.valueChanges.pipe(
        startWith(""),
        map(value => this._filter(value))
      );
      this.dataSource.paginator = this.paginator;
      callback();
    }, 0);
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.options.filter(option =>
      option.toLowerCase().includes(filterValue)
    );
  }
  // Search AutoComplete [End]

  // Data list [Start]
  displayedColumns: string[] = [
    "patientCode",
    "name",
    "phoneNumber",
    "ssn",
    "control"
  ];
  dataSource = new MatTableDataSource<PatientElement>(this.patientGroups);

  // Data list [End]

  onUpdate() {
    alert("Clicked");
  }

  onDisable() {
    this.modal.open(NgbdModalComponent);
  }

  onViewTreatment() {
    alert("Clicked");
  }
}
