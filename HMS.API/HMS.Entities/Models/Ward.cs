using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class Ward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LatiLongTude { get; set; }
        public int DistrictID { get; set; }
        public int SortOrder { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsDeleted { get; set; }

        public District District { get; set; }
    }
}
