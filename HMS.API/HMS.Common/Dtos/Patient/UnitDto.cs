using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Common.Dtos.Patient
{
    public class UnitDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
