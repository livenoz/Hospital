using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TPatient
    {
        public TPatient()
        {
            TMedicalRecord = new HashSet<TMedicalRecord>();
            TPrescription = new HashSet<TPrescription>();
            TPrescriptionDetail = new HashSet<TPrescriptionDetail>();
            TTreatment = new HashSet<TTreatment>();
            TTreatmentDetail = new HashSet<TTreatmentDetail>();
            TTreatmentDisease = new HashSet<TTreatmentDisease>();
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
        public string FirstRelativeName { get; set; }
        public string FirstRelativeIdentifyCardNo { get; set; }
        public string FirstRelativePhone { get; set; }
        public string FirstRelativeDescription { get; set; }
        public string SecondRelativeName { get; set; }
        public string SecondRelativeIdentifyCardNo { get; set; }
        public string SecondRelativePhone { get; set; }
        public string SecondRelativeDescription { get; set; }
        public string DiseaseHistories { get; set; }
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
        public ICollection<TPrescription> TPrescription { get; set; }
        public ICollection<TPrescriptionDetail> TPrescriptionDetail { get; set; }
        public ICollection<TTreatment> TTreatment { get; set; }
        public ICollection<TTreatmentDetail> TTreatmentDetail { get; set; }
        public ICollection<TTreatmentDisease> TTreatmentDisease { get; set; }
    }
}
