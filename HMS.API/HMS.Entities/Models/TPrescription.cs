using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TPrescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int MedicalRecordId { get; set; }
        public int TreatmentId { get; set; }
        public int DoctorId { get; set; }
        public string Diagnose { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }

        public TEmployee Doctor { get; set; }
        public TMedicalRecord MedicalRecord { get; set; }
        public TPatient Patient { get; set; }
        public TTreatment Treatment { get; set; }
    }
}
