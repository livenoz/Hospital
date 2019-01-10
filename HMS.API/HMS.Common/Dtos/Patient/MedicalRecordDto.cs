using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Common.Dtos.Patient
{
    public class MedicalRecordDto
    {
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
        public string StatusName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFullName => $"{PatientFirstName} {PatientLastName}";
    }
}
