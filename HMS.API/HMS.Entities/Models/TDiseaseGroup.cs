using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TDiseaseGroup
    {
        public TDiseaseGroup()
        {
            TDisease = new HashSet<TDisease>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<TDisease> TDisease { get; set; }
    }
}
