using HMS.Common.Dtos.User;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace HMS.API.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static AuthenticationDto ToAuthenticationDto(this ClaimsPrincipal source)
            => JsonConvert.DeserializeObject<AuthenticationDto>(source
                .Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value);
    }
}
