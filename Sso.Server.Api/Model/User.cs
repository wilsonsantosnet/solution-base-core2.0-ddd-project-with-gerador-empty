using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sso.Server.Api.Model
{
    public class User
    {
        public string SubjectId { get; internal set; }
        public string Username { get; internal set; }
        public string Password { get; internal set; }
        public List<Claim> Claims { get; internal set; }
    }
}
