using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TRole
    {
        public TRole()
        {
            TAccessRight = new HashSet<TAccessRight>();
            TUser = new HashSet<TUser>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<TAccessRight> TAccessRight { get; set; }
        public ICollection<TUser> TUser { get; set; }
    }
}
