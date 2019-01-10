export class TreatmentModel {
    id: number;
    patientId: number;
    patientFirstName: string;
    patientLastName: string;
    medicalRecordId: number;
    medicalRecordCode: string;
    startDate: Date;
    endDate: Date;
    title: string;
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
