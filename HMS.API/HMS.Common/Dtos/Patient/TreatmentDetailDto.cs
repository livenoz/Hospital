using System.Collections.Generic;

namespace HMS.Common.Dtos.Patient
{
    public class TreatmentDetailDto: TreatmentDto
    {
        public List<DiseaseDto> Diseases { get; set; }
    }
}
