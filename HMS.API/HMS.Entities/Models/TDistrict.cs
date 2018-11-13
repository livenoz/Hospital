using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TDistrict
    {
        public TDistrict()
        {
            TBranch = new HashSet<TBranch>();
            TEmployeeDistrict = new HashSet<TEmployee>();
            TEmployeeNativeDistrict = new HashSet<TEmployee>();
            TPatientDistrict = new HashSet<TPatient>();
            TPatientNativeDistrict = new HashSet<TPatient>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }

        public TProvince Province { get; set; }
        public ICollection<TBranch> TBranch { get; set; }
        public ICollection<TEmployee> TEmployeeDistrict { get; set; }
        public ICollection<TEmployee> TEmployeeNativeDistrict { get; set; }
        public ICollection<TPatient> TPatientDistrict { get; set; }
        public ICollection<TPatient> TPatientNativeDistrict { get; set; }
    }
}
