using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Api.Models
{
    public class AuthenticationModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string[] Roles { get; set; }

        public string AuthenType { get; set; }
    }
}