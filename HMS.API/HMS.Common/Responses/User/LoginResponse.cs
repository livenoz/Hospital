using HMS.Common.Enums;

namespace HMS.Common.Responses.User
{
    public class LoginResponse
    {
        public EResponseStatus Status { get; set; }

        public string AccessToken { get; set; }
    }
}
