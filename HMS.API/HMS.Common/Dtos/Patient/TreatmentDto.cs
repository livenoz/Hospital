﻿using System;

namespace HMS.Common.Dtos.Patient
{
    public class TreatmentDto
    {
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
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string NurseFirstName { get; set; }
        public string NurseLastName { get; set; }
    }
}
