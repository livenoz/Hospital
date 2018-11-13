using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TRight
    {
        public TRight()
        {
            TAccessRight = new HashSet<TAccessRight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<TAccessRight> TAccessRight { get; set; }
    }
}
