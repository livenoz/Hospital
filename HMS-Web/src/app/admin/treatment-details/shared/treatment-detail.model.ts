export class TreatmentDetailModel {
    id: number;
    patientId: number;
    patientFirstName: string;
    patientLastName: string;
    medicalRecordId: number;
    medicalRecordCode: string;
    treatmentId: number;
    treatmentCode: string;
    startDate: Date;
    endDate: Date;
    result: string;
    content: string;
    doctorId: number;
    doctorFirstName: string;
    doctorLastName: string;
    nurseId: number;
    nurseFirstName: string;
    nurseLastName: string;
    note: string;
    createdBy: number;
    createdTime: Date;
    updatedBy: number;
    updatedTime: Date;
    isActived: boolean;
    isDeleted: boolean;
}
