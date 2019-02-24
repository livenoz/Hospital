import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { PatientModel } from '../patients/shared/patient.model';
import { Observable } from 'rxjs';
import { MedicalRecordModel } from '../medical-records/shared/medical-record.model';
import { TreatmentModel } from '../treatments/shared/treatment.model';
import { TreatmentDetailModel } from '../treatment-details/shared/treatment-detail.model';
import { PatientService } from '../patients/shared/patient.service';
import { MedicalRecordService } from '../medical-records/shared/medical-record.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginatedListModel } from '../../shared/models/paginated-list.model';
import { NgbdModalComponent } from '../../shared/modules/modal/modal-component';
import { Constants } from '../../shared/constants/constants';
import { PrescriptionsService } from './shared/prescriptions.service';
import { UnitModel } from './shared/unit.model';
import { DrugModel } from './shared/drug.model';
import { PrescriptionDetailModel } from './shared/prescription-detail.model';

@Component({
  selector: 'app-prescriptions',
  templateUrl: './prescriptions.component.html',
  styleUrls: ['./prescriptions.component.scss']
})
export class PrescriptionsComponent implements OnInit {
  myForm: FormGroup;
  submitted = false;
  prescriptionsControl = 'Cập nhật toa thuốc';
  // patientControl: FormControl;
  // medicalRecordControl: FormControl;
  // treatmentControl: FormControl;
  // resultControl: FormControl;
  // contentControl: FormControl;
  // doctorControl: FormControl;
  // nurseControl: FormControl;
  // startDateControl: FormControl;
  // endDateControl: FormControl;
  // prescriptionDetailsControl: FormArray;
  drugControls: FormControl[];
  unitControls: FormControl[];
  amountControls: FormControl[];
  filteredDrugs: Observable<DrugModel[]>[];
  filteredUnits: Observable<UnitModel[]>[];
  patients: PatientModel[];
  medicalRecords: MedicalRecordModel[];
  treatments: TreatmentModel[];
  drugs: DrugModel[];
  units: UnitModel[];
  patientRouteId: number;
  public inputDatas: PrescriptionDetailModel[];
  isLoadingPatient = false;
  treatmentDetailRouteId: number;
  treatmentRouteId: number;

  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private prescriptionService: PrescriptionsService,
    private patientService: PatientService,
    private medicalRecordService: MedicalRecordService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router) {

    this.inputDatas = [];
    // this.patientControl = new FormControl();
    // this.medicalRecordControl = new FormControl();
    // this.treatmentControl = new FormControl();
    // this.resultControl = new FormControl();
    // this.contentControl = new FormControl();
    // this.doctorControl = new FormControl();
    // this.nurseControl = new FormControl();
    // this.startDateControl = new FormControl();
    // this.endDateControl = new FormControl();
    this.drugControls = [];
    this.unitControls = [];
    this.amountControls = [];
    this.filteredDrugs = [];
    this.filteredUnits = [];
  }

  ngOnInit() {
    // tslint:disable-next-line:radix
    this.treatmentDetailRouteId = parseInt(this.route.params['value'].treatmentDetailId);
    // tslint:disable-next-line:radix
    this.treatmentRouteId = parseInt(this.route.params['value'].treatmentId);
    this.dropdownConfig(() => this.spinner.hide());
    this.myForm = this.formBuilder.group({
      prescriptionDetailsControl: this.formBuilder.array([])
    });
  }

  private dropdownConfig(callback) {
    this.prescriptionService.getAllDrugs()
      .subscribe((drugs: PaginatedListModel<DrugModel>) => {
        this.drugs = drugs.items;
        this.prescriptionService.getAllUnits()
          .subscribe((units: PaginatedListModel<UnitModel>) => {
            this.units = units.items;
            const params = {
              treatmentDetailId: this.treatmentDetailRouteId
            };
            this.prescriptionService.getByTreatmentDetailId(params)
              .subscribe((prescriptionDetails: PaginatedListModel<PrescriptionDetailModel>) => {
                this.inputDatas = prescriptionDetails.items;
                if (this.inputDatas == null || this.inputDatas.length === 0) {
                  this.addPrescriptionDetail();
                } else {
                  this.inputDatas.forEach(inputData => {
                    this.initItemControl();
                  });
                }
              });
          });
      });


    callback();
  }


  private filterDrug(value: string) {
    const valueFilter = (value || '').toString();
    return this.drugs.filter(c => this.checkMatchStringFilter(c.name, valueFilter));
  }

  private filterUnit(value: string) {
    const valueFilter = (value || '').toString();
    return this.units.filter(c => this.checkMatchStringFilter(c.name, valueFilter));
  }

  public getMedicalRecords = (): Observable<PaginatedListModel<MedicalRecordModel>> => {
    const params = {
      pageIndex: 0,
      pageSize: 2000
    };
    return this.medicalRecordService.getBeingTreated(params);
  }

  public getPatients = (value: string): Observable<PaginatedListModel<PatientModel>> => {
    const params = {
      pageIndex: 0,
      pageSize: 2000,
      code: 'name',
      value: typeof (value) === 'string' && value ? value.trim() : '',
    };
    return this.patientService.get(params);
  }

  public displayDrug = (drugId: number): string => {
    const model = this.drugs ? this.drugs.find(c => c.id === drugId) : null;
    return model ? model.name : '';
  }

  public displayUnit = (unitId: number): string => {
    const model = this.units ? this.units.find(c => c.id === unitId) : null;
    return model ? model.name : '';
  }

  public selectionChangeDrug = (drug: DrugModel, prescriptionDetail: PrescriptionDetailModel): void => {
    prescriptionDetail.unitId = drug.unitId;
    prescriptionDetail.amountAfternoon = drug.amountAfternoon;
    prescriptionDetail.amountEvening = drug.amountEvening;
    prescriptionDetail.amountMorning = drug.amountMorning;
    prescriptionDetail.note = drug.note;
  }

  public onSubmit(): void {
    if (this.myForm.invalid) {
      this.submitted = true;
      return;
    }
    console.log(this.myForm);
    this.onUpdate();
  }

  public addPrescriptionDetail(): void {
    this.inputDatas.push(new PrescriptionDetailModel());
    this.initItemControl();
  }

  private initItemControl() {
    const controls = <FormArray>this.myForm.controls.prescriptionDetailsControl;

    const control = this.formBuilder.group({
      drugControl: [0, Validators.required],
      unitControl: [0, Validators.required],
      amountControl: [0, Validators.required]
    });
    const filteredDrug = control.controls.drugControl.valueChanges.pipe(
      startWith(''),
      map(value => this.filterDrug(value)));
    this.filteredDrugs.push(filteredDrug);

    const unitControl = new FormControl();
    const filteredUnit = control.controls.unitControl.valueChanges.pipe(
      startWith(''),
      map(value => this.filterUnit(value)));
    this.filteredUnits.push(filteredUnit);
    controls.push(control);
  }

  public removePrescriptionDetail(index: number): void {
    if (index < this.inputDatas.length) {
      this.inputDatas.splice(index, 1);
    }
    const controls = <FormArray>this.myForm.controls.prescriptionDetailsControl;
    controls.removeAt(index);
    this.drugControls.splice(index, 1);
    this.unitControls.splice(index, 1);
    this.amountControls.splice(index, 1);
    this.filteredDrugs.splice(index, 1);
    this.filteredUnits.splice(index, 1);
  }

  private checkMatchStringFilter = (source: string, searchString: string): boolean => {
    // tslint:disable-next-line:max-line-length
    return this.removeUnicode(source ? source.toLowerCase() : '').indexOf(this.removeUnicode(searchString ? searchString.toLowerCase() : '')) >= 0;
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
    const pamams = {
      treatmentDetailId: this.treatmentDetailRouteId,
      prescriptionDetails: this.inputDatas
    };
    this.prescriptionService.update(pamams).subscribe(
      (data: boolean) => {
        this.spinner.hide();
        if (data) {
          const modalRef = this.modal.open(NgbdModalComponent);
          modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
          modalRef.componentInstance.isDisplayCancel = false;
          modalRef.result.then(_result => {
            this.router.navigate(['/treatment-details', this.treatmentRouteId]);
          });
        } else {
          const modalRef = this.modal.open(NgbdModalComponent);
          modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef.componentInstance.content = Constants.MODAL.UPDATE_FAIL;
          modalRef.componentInstance.isDisplayCancel = false;
        }
      },
      () => {
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
