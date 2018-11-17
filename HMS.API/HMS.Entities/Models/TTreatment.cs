using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TTreatment
    {
        public TTreatment()
        {
            TPrescription = new HashSet<TPrescription>();
            TTreatmentDisease = new HashSet<TTreatmentDisease>();
        }

        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? DoctorId { get; set; }
        public int? NurseId { get; set; }
        public string Content { get; set; }
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
        public ICollection<TPrescription> TPrescription { get; set; }
        public ICollection<TTreatmentDisease> TTreatmentDisease { get; set; }
    }
}
