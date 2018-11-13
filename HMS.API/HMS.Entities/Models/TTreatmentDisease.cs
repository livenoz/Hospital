using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TTreatmentDisease
    {
        public int Id { get; set; }
        public int TreatmentId { get; set; }
        public int DiseaseId { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TDisease Disease { get; set; }
        public TTreatment Treatment { get; set; }
    }
}
