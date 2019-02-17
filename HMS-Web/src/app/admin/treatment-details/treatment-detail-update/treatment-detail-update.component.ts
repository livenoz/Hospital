import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalComponent } from '../../../shared/modules/modal/modal-component';
import { Constants } from '../../../shared/constants/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../../patients/shared/patient.service';
import { PatientModel } from '../../patients/shared/patient.model';
import { Observable } from 'rxjs';
import { PaginatedListModel } from '../../../shared/models/paginated-list.model';
import { TreatmentDetailService } from '../shared/treatment-detail.service';
import { MedicalRecordModel } from '../../medical-records/shared/medical-record.model';
import { MedicalRecordService } from '../../medical-records/shared/medical-record.service';
import { EmployeeService } from '../../../shared/services/employee.service';
import { DoctorModel } from '../../../shared/models/employee/nurse.model';
import { NurseModel } from '../../../shared/models/employee/doctor.model';
import { TreatmentDetailModel } from '../shared/treatment-detail.model';
import { TreatmentService } from '../../treatments/shared/treatment.service';
import { TreatmentDiseaseModel } from '../../treatments/shared/treatment-disease.model';
import { TreatmentModel } from '../../treatments/shared/treatment.model';

@Component({
  selector: 'app-treatment-detail-update',
  templateUrl: './treatment-detail-update.component.html',
  styleUrls: ['./treatment-detail-update.component.scss']
})
export class TreatmentDetailUpdateComponent implements OnInit {
  myForm: FormGroup;
  submitted = false;
  treatmentDetailControl = 'Cập nhật điều trị chi tiết';
  patientControl: FormControl;
  medicalRecordControl: FormControl;
  treatmentControl: FormControl;
  resultControl: FormControl;
  contentControl: FormControl;
  doctorControl: FormControl;
  nurseControl: FormControl;
  startDateControl: FormControl;
  endDateControl: FormControl;
  filteredPatient: Observable<PatientModel[]>;
  filteredMedicalRecord: Observable<MedicalRecordModel[]>;
  filteredTreatment: Observable<TreatmentModel[]>;
  filteredDoctor: Observable<DoctorModel[]>;
  filteredNurse: Observable<NurseModel[]>;
  patients: PatientModel[];
  medicalRecords: MedicalRecordModel[];
  treatments: TreatmentModel[];
  doctors: DoctorModel[];
  nurses: NurseModel[];
  patientRouteId: number;
  public inputData: TreatmentDetailModel;
  isLoadingPatient = false;
  treatmentDetailRouteId: number;

  constructor(
    private spinner: NgxSpinnerService,
    private modal: NgbModal,
    private patientService: PatientService,
    private medicalRecordService: MedicalRecordService,
    private treatmentDetailService: TreatmentDetailService,
    private treatmentService: TreatmentService,
    private employeeService: EmployeeService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router) {

    this.inputData = new TreatmentDetailModel();
    this.patientControl = new FormControl();
    this.medicalRecordControl = new FormControl();
    this.treatmentControl = new FormControl();
    this.resultControl = new FormControl();
    this.contentControl = new FormControl();
    this.doctorControl = new FormControl();
    this.nurseControl = new FormControl();
    this.startDateControl = new FormControl();
    this.endDateControl = new FormControl();
  }

  ngOnInit() {
    // tslint:disable-next-line:radix
    this.treatmentDetailRouteId = parseInt(this.route.params['value'].id);
    this.dropdownConfig(() => this.spinner.hide());
    this.inputData.startDate = new Date();
    this.inputData.endDate = new Date();
    this.myForm = this.formBuilder.group({
      patientControl: [0, Validators.required],
      medicalRecordControl: [0, Validators.required],
      resultControl: ['', Validators.required],
      contentControl: ['', Validators.required],
      startDateControl: ['', Validators.required],
      endDateControl: ['', Validators.required]
    });
  }

  private dropdownConfig(callback) {
    this.treatmentDetailService.getDetail(this.treatmentDetailRouteId)
      .subscribe((treatmentDetail: TreatmentDetailModel) => {
        this.treatmentService.getDetail(treatmentDetail.treatmentId)
          .subscribe((treatment: TreatmentModel) => {

            const patient = new PatientModel();
            patient.id = treatment.patientId;
            patient.firstName = treatment.patientFirstName;
            patient.lastName = treatment.patientLastName;
            patient.fullName = `${patient.firstName} ${patient.lastName}`;
            this.patients = [patient];

            const medicalRecord = new MedicalRecordModel();
            medicalRecord.id = treatment.medicalRecordId;
            medicalRecord.code = treatment.medicalRecordCode;
            medicalRecord.patientId = treatment.patientId;
            this.medicalRecords = [medicalRecord];

            this.treatments = [treatment];
            this.filteredPatient = this.patientControl.valueChanges.pipe(
              startWith(''),
              map(value => this.filterPatient(value)));
            this.filteredMedicalRecord = this.medicalRecordControl.valueChanges.pipe(
              startWith(''),
              map(value => this.filterMedicalRecord(value)));

            this.filteredTreatment = this.treatmentControl.valueChanges.pipe(
              startWith(''),
              map(value => this.filterTreatment(value)));
            this.inputData = treatmentDetail;
          });
      });


    this.employeeService.getAllDoctors()
      .subscribe((doctors: PaginatedListModel<DoctorModel>) => {
        this.doctors = doctors.items;
        this.filteredDoctor = this.doctorControl.valueChanges.pipe(
          startWith(''),
          map(value => this.filterDoctor(value)));
      });

    this.employeeService.getAllNurses()
      .subscribe((nurses: PaginatedListModel<DoctorModel>) => {
        this.nurses = nurses.items;
        this.filteredNurse = this.nurseControl.valueChanges.pipe(
          startWith(''),
          map(value => this.filterNurse(value)));
      });
    callback();
  }

  private filterPatient(value: string) {
    const valueFilter = (value || '').toString();
    return this.patients.filter(c => this.checkMatchStringFilter(c.fullName, valueFilter));
  }

  private filterDoctor(value: string) {
    const valueFilter = (value || '').toString();
    return this.doctors.filter(c => this.checkMatchStringFilter(c.firstName + ' ' + c.lastName, valueFilter));
  }

  private filterNurse(value: string) {
    const valueFilter = (value || '').toString();
    return this.nurses.filter(c => this.checkMatchStringFilter(c.firstName + ' ' + c.lastName, valueFilter));
  }

  private filterMedicalRecord = (value: string) => {
    const valueFilter = (value || '').toString();
    return this.medicalRecords.filter(c => c.patientId === this.inputData.patientId && this.checkMatchStringFilter(c.code, valueFilter));
  }

  private filterTreatment = (value: string) => {
    const valueFilter = (value || '').toString();
    return this.treatments.filter(c => c.medicalRecordId === this.inputData.patientId && this.checkMatchStringFilter(c.code, valueFilter));
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

  public displayPatient = (patientId: number): string => {
    const patient = this.patients ? this.patients.find(c => c.id === patientId) : null;
    return patient ? patient.fullName : '';
  }

  public displayMedicalRecord = (medicalRecordId: number): string => {
    const medicalRecord = this.medicalRecords ? this.medicalRecords.find(c => c.id === medicalRecordId) : null;
    return medicalRecord ? medicalRecord.code : '';
  }

  public displayTreatment = (treatmentId: number): string => {
    const treatment = this.treatments ? this.treatments.find(c => c.id === treatmentId) : null;
    return treatment ? treatment.code : '';
  }

  public displayDoctor = (medicalRecordId: number): string => {
    const model = this.doctors ? this.doctors.find(c => c.id === medicalRecordId) : null;
    return model ? model.firstName + ' ' + model.lastName : '';
  }

  public displayNurse = (medicalRecordId: number): string => {
    const model = this.nurses ? this.nurses.find(c => c.id === medicalRecordId) : null;
    return model ? model.firstName + ' ' + model.lastName : '';
  }

  public onSubmit(): void {
    if (this.myForm.invalid) {
      this.submitted = true;
      return;
    }
    this.onUpdate();
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
    this.treatmentDetailService.update(this.inputData).subscribe(
      (data: boolean) => {
        this.spinner.hide();
        if (data) {
          const modalRef = this.modal.open(NgbdModalComponent);
          modalRef.componentInstance.header = Constants.MODAL.INFORMATION;
          modalRef.componentInstance.content = Constants.MODAL.UPDATE_SUCCESS;
          modalRef.componentInstance.isDisplayCancel = false;
          modalRef.result.then(_result => {
            this.router.navigate(['/treatment-details', this.inputData.treatmentId]);
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
