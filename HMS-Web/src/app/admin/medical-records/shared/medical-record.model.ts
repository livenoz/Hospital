export class MedicalRecordModel {
    id: number;
    patientFirstName: string;
    patientId: number;
    patientLastName: string;
    reason: string;
    startDate: Date;
    statusId: number;
    statusName: string;
    createdBy: number;
    createdTime: Date;
    endDate: Date;
    updatedBy: number;
    updatedTime: Date;
    isActived: boolean;
    isDeleted: boolean;
    fullName() { return '${this.patientFirstName} ${this.patientLastName}'; }
}
