using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TDepartment
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TBranch Branch { get; set; }
    }
}
