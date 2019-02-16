using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TMedicalRecord
    {
        public TMedicalRecord()
        {
            TPrescription = new HashSet<TPrescription>();
            TTreatment = new HashSet<TTreatment>();
            TTreatmentDisease = new HashSet<TTreatmentDisease>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TPatient Patient { get; set; }
        public TMedicalRecordStatus Status { get; set; }
        public ICollection<TPrescription> TPrescription { get; set; }
        public ICollection<TTreatment> TTreatment { get; set; }
        public ICollection<TTreatmentDisease> TTreatmentDisease { get; set; }
    }
}
