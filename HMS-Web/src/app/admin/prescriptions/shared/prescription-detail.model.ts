export class PrescriptionDetailModel {
    id: number;
    patientId: number;
    medicalRecordId: number;
    treatmentId: number;
    treatmentDetailId: number;
    drugId: number;
    amount: number;
    unitId: number;
    useId: number;
    amountMorning: number;
    amountAfternoon: number;
    amountEvening: number;
    diagnose: string;
    note: string;
    createdTime: Date;
    createdBy: number;
    updatedTime: Date;
    updatedBy: number;
    isActived: boolean;
    drugName: string;
    unitName: string;
}
