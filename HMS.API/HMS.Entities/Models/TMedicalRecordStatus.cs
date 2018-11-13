using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TMedicalRecordStatus
    {
        public TMedicalRecordStatus()
        {
            TMedicalRecord = new HashSet<TMedicalRecord>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<TMedicalRecord> TMedicalRecord { get; set; }
    }
}
