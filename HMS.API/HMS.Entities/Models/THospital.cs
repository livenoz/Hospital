using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class THospital
    {
        public THospital()
        {
            TBranch = new HashSet<TBranch>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string Website { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<TBranch> TBranch { get; set; }
    }
}
