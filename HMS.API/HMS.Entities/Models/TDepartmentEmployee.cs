using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TDepartmentEmployee
    {
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
    }
}
