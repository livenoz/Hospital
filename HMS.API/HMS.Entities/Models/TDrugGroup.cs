using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TDrugGroup
    {
        public TDrugGroup()
        {
            TDrug = new HashSet<TDrug>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<TDrug> TDrug { get; set; }
    }
}
