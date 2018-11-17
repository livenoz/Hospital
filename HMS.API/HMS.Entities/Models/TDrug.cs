using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TDrug
    {
        public TDrug()
        {
            TPrescriptionDetail = new HashSet<TPrescriptionDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public int UnitId { get; set; }
        public int UseId { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }

        public TDrugGroup Group { get; set; }
        public TUnit Unit { get; set; }
        public TDrugUse Use { get; set; }
        public ICollection<TPrescriptionDetail> TPrescriptionDetail { get; set; }
    }
}
