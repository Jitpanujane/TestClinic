using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Project.Api.Models
{
    public class PrincipalModel : IPrincipal
    {
        private readonly IdentityModel identity;

        public PrincipalModel(IdentityModel identity)
        {
            this.identity = identity;
        }

        public IIdentity Identity => identity;

        public bool IsInRole(string role)
        {
            return identity != null 
                && identity.Roles != null
                && identity.Roles.Contains(role);
        }
    }
}