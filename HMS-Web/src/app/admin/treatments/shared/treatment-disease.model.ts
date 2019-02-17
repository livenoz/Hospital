import { DiseaseModel } from './disease.model';
import { TreatmentModel } from './treatment.model';

export class TreatmentDiseaseModel extends TreatmentModel {
    diseases: DiseaseModel[];
}
