using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TAccessRight
    {
        public int RoleId { get; set; }
        public int RightId { get; set; }
        public int Privilege { get; set; }

        public TRight Right { get; set; }
        public TRole Role { get; set; }
    }
}
