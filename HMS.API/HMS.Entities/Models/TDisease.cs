using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TDisease
    {
        public TDisease()
        {
            TTreatmentDisease = new HashSet<TTreatmentDisease>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TDiseaseGroup Group { get; set; }
        public ICollection<TTreatmentDisease> TTreatmentDisease { get; set; }
    }
}
