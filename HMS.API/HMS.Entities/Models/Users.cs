using System;
using System.Collections.Generic;

namespace HMS.Entities.Models
{
    public partial class Users
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Ssn { get; set; }
    }
}
