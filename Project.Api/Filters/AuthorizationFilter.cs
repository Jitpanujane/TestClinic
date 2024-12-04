using Project.Api.Models;
using Project.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Project.Api.Filters
{
    public class AuthorizationFilter : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorization = request.Headers.Authorization;

            if (authorization?.Scheme.ToLower() == "bearer")
            {
                Authorization(authorization.Parameter);
            }

            return base.SendAsync(request, cancellationToken);
        }

        private void Authorization(string token)
        {
            var currentUser = JwtHandler.Decode<AuthenticationModel>(token);
            var identity = new IdentityModel(currentUser);

            if (identity.IsAuthenticated)
            {
                var principal = new PrincipalModel(identity);
                SetPrincipal(principal);
            }
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }
    }
}