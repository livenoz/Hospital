using HMS.Business.Interfaces;
using HMS.Common.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HMS.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class BearerAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly bool _isAuthen;

        public BearerAuthorizeAttribute(bool isAuthen = true)
        {
            _isAuthen = isAuthen;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (_isAuthen)
            {
                AuthenticationDto authenTicket = null;
                var hasAuthorizationHeader = context.HttpContext.Request.Headers.ContainsKey("Authorization");
                if (hasAuthorizationHeader)
                {
                    var authHeader = AuthenticationHeaderValue.Parse(context.HttpContext.Request.Headers["Authorization"]);
                    if (authHeader != null && authHeader.Scheme == "Bearer" && !string.IsNullOrEmpty(authHeader.Parameter))
                    {
                        var service = context.HttpContext.RequestServices.GetService(typeof(IUserBusiness)) as IUserBusiness;
                        authenTicket = service.CheckAuthentication(authHeader.Parameter);
                        var claims = new[] 
                        {
                            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(authenTicket))
                        };
                        var identity = new ClaimsIdentity(claims, authHeader.Scheme);
                        var principal = new ClaimsPrincipal(identity);
                        context.HttpContext.User = principal;
                        Thread.CurrentPrincipal = principal;
                    }
                }
                if (authenTicket == null)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
