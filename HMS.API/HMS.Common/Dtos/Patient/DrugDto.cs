using System;
using System.Collections.Generic;
using System.Text;

namespace HMS.Common.Dtos.Patient
{
    public class DrugDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public int UnitId { get; set; }
        public int? UseId { get; set; }
        public int? AmountMorning { get; set; }
        public int? AmountAfternoon { get; set; }
        public int? AmountEvening { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public string UnitName { get; set; }
    }
}
