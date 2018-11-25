using System;

namespace HMS.Common.Dtos.User
{
    public class AuthenticationDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
