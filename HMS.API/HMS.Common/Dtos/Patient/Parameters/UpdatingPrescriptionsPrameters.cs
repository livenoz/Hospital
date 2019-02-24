using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Common.Dtos.Patient.Parameters
{
    public class UpdatingPrescriptionsPrameters
    {
        public int TreatmentDetailId { get; set; }
        public List<PrescriptionDetailDto> PrescriptionDetails { get; set; }
    }
}
