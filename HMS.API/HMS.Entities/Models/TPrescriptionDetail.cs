using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TPrescriptionDetail
    {
        public int Id { get; set; }
        public int PrescriptionId { get; set; }
        public int DrugId { get; set; }
        public double Amount { get; set; }
        public int UnitId { get; set; }
        public int UseId { get; set; }
        public string Diagnose { get; set; }
        public string Note { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }

        public TDrug Drug { get; set; }
        public TPrescription Prescription { get; set; }
        public TUnit Unit { get; set; }
        public TDrugUse Use { get; set; }
    }
}
