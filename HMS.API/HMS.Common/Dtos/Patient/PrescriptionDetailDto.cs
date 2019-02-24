using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Common.Dtos.Patient
{
    public class PrescriptionDetailDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int MedicalRecordId { get; set; }
        public int TreatmentId { get; set; }
        public int TreatmentDetailId { get; set; }
        public int DrugId { get; set; }
        public double Amount { get; set; }
        public int UnitId { get; set; }
        public int? UseId { get; set; }
        public int? AmountMorning { get; set; }
        public int? AmountAfternoon { get; set; }
        public int? AmountEvening { get; set; }
        public string Diagnose { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }
        public string DrugName { get; set; }
        public string UnitName { get; set; }
    }
}
