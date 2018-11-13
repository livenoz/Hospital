using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TEmployeeType
    {
        public TEmployeeType()
        {
            TEmployee = new HashSet<TEmployee>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<TEmployee> TEmployee { get; set; }
    }
}
