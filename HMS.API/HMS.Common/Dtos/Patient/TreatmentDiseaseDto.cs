using System.Collections.Generic;

namespace HMS.Common.Dtos.Patient
{
    public class TreatmentDiseaseDto: TreatmentDto
    {
        public List<DiseaseDto> Diseases { get; set; }
    }
}
