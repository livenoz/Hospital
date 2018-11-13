using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TBranch
    {
        public TBranch()
        {
            TDepartment = new HashSet<TDepartment>();
            TEmployee = new HashSet<TEmployee>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int HospitalId { get; set; }
        public int TypeId { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TCountry Country { get; set; }
        public TDistrict District { get; set; }
        public THospital Hospital { get; set; }
        public TProvince Province { get; set; }
        public TBranchType Type { get; set; }
        public ICollection<TDepartment> TDepartment { get; set; }
        public ICollection<TEmployee> TEmployee { get; set; }
    }
}
