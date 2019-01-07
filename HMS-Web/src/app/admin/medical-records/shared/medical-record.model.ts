export class MedicalRecordModel {
    id: number;
    patientId: number;
    patientLastName: string;
    patientFirstName: string;
    patientFullName: string;
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
}
