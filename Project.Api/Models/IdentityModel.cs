using Project.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Project.Api.Models
{
    public class IdentityModel : IIdentity
    {
        private readonly AuthenticationModel data;

        public IdentityModel(AuthenticationModel data)
        {
            this.data = data;
        }

        public int Id => data?.Id ?? 0;

        public string[] Roles => data?.Roles;

        public string Name => data?.Name;

        public string AuthenticationType => data?.AuthenType;

        public bool IsAuthenticated => data != null && data.Id > 0;
    }
}