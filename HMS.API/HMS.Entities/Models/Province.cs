using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class Province
    {
        public Province()
        {
            District = new HashSet<District>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? TelephoneCode { get; set; }
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }

        public Country Country { get; set; }
        public ICollection<District> District { get; set; }
    }
}
