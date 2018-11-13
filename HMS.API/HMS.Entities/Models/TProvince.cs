using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TProvince
    {
        public TProvince()
        {
            TBranch = new HashSet<TBranch>();
            TDistrict = new HashSet<TDistrict>();
            TEmployeeNativeProvince = new HashSet<TEmployee>();
            TEmployeeProvince = new HashSet<TEmployee>();
            TPatientNativeProvince = new HashSet<TPatient>();
            TPatientProvince = new HashSet<TPatient>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public TCountry Country { get; set; }
        public ICollection<TBranch> TBranch { get; set; }
        public ICollection<TDistrict> TDistrict { get; set; }
        public ICollection<TEmployee> TEmployeeNativeProvince { get; set; }
        public ICollection<TEmployee> TEmployeeProvince { get; set; }
        public ICollection<TPatient> TPatientNativeProvince { get; set; }
        public ICollection<TPatient> TPatientProvince { get; set; }
    }
}
