using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TCountry
    {
        public TCountry()
        {
            TBranch = new HashSet<TBranch>();
            TEmployeeCountry = new HashSet<TEmployee>();
            TEmployeeNativeCountry = new HashSet<TEmployee>();
            TPatientCountry = new HashSet<TPatient>();
            TPatientNativeCountry = new HashSet<TPatient>();
            TProvince = new HashSet<TProvince>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<TBranch> TBranch { get; set; }
        public ICollection<TEmployee> TEmployeeCountry { get; set; }
        public ICollection<TEmployee> TEmployeeNativeCountry { get; set; }
        public ICollection<TPatient> TPatientCountry { get; set; }
        public ICollection<TPatient> TPatientNativeCountry { get; set; }
        public ICollection<TProvince> TProvince { get; set; }
    }
}
