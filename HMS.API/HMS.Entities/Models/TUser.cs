using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class TUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string AccessToken { get; set; }
        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }

        public TRole Role { get; set; }
    }
}
