using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TPatient
    {
        public TPatient()
        {
            TMedicalRecord = new HashSet<TMedicalRecord>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string Address { get; set; }
        public int NativeCountryId { get; set; }
        public int NativeProvinceId { get; set; }
        public int NativeDistrictId { get; set; }
        public string NativeAddress { get; set; }
        public int Sex { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string IdentifyCardNo { get; set; }
        public DateTime? DateOfIssue { get; set; }
        public string PlaceOfIssue { get; set; }
        public string ImageUrl { get; set; }
        public string FatherName { get; set; }
        public string FatherIdentifyCardNo { get; set; }
        public string MotherName { get; set; }
        public string MotherIdentifyCardNo { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TCountry Country { get; set; }
        public TDistrict District { get; set; }
        public TCountry NativeCountry { get; set; }
        public TDistrict NativeDistrict { get; set; }
        public TProvince NativeProvince { get; set; }
        public TProvince Province { get; set; }
        public ICollection<TMedicalRecord> TMedicalRecord { get; set; }
    }
}
