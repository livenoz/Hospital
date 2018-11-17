using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TUnit
    {
        public TUnit()
        {
            TDrug = new HashSet<TDrug>();
            TPrescriptionDetail = new HashSet<TPrescriptionDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TDrug> TDrug { get; set; }
        public ICollection<TPrescriptionDetail> TPrescriptionDetail { get; set; }
    }
}
