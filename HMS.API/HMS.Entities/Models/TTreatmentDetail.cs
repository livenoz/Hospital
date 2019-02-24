using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TTreatmentDetail
    {
        public TTreatmentDetail()
        {
            TPrescriptionDetail = new HashSet<TPrescriptionDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int PatientId { get; set; }
        public int MedicalRecordId { get; set; }
        public int TreatmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DoctorId { get; set; }
        public int? NurseId { get; set; }
        public string Content { get; set; }
        public string Result { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TEmployee Doctor { get; set; }
        public TMedicalRecord MedicalRecord { get; set; }
        public TEmployee Nurse { get; set; }
        public TPatient Patient { get; set; }
        public TTreatment Treatment { get; set; }
        public ICollection<TPrescriptionDetail> TPrescriptionDetail { get; set; }
    }
}
