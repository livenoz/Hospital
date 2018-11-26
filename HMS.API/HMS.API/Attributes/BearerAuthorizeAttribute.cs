using HMS.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
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
                var isAuthenticated = false;
                var hasAuthorizationHeader = context.HttpContext.Request.Headers.ContainsKey("Authorization");
                if (hasAuthorizationHeader)
                {
                    var authHeader = AuthenticationHeaderValue.Parse(context.HttpContext.Request.Headers["Authorization"]);
                    if (authHeader != null && authHeader.Scheme == "Bearer" && !string.IsNullOrEmpty(authHeader.Parameter))
                    {
                        var service = context.HttpContext.RequestServices.GetService(typeof(IUserBusiness)) as IUserBusiness;
                        isAuthenticated = service.CheckAuthentication(authHeader.Parameter);
                    }
                }
                if (!isAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
