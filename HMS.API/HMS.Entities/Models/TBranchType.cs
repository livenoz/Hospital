using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TBranchType
    {
        public TBranchType()
        {
            TBranch = new HashSet<TBranch>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TBranch> TBranch { get; set; }
    }
}
