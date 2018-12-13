import { PatientModel } from './PatientModel';
export class PatientsResponse {
    pageIndex: number;
    totalPage: number;
    items: PatientModel[];
    count: number;
}
