export class MedicalRecordModel {
    id: number;
    code: string;
    patientId: number;
    patientLastName: string;
    patientFirstName: string;
    patientFullName: string;
    diagnose: string;
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
