import { DiseaseModel } from './disease.model';
import { TreatmentModel } from './treatment.model';

export class TreatmentDetailModel extends TreatmentModel {
    diseases: DiseaseModel[];
}
