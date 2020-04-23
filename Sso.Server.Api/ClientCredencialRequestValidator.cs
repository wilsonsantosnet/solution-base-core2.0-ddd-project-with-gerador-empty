using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sso.Server.Api
{
    public class ClientCredentialRequestValidator : ICustomTokenRequestValidator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClientCredentialRequestValidator(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            var client = context.Result.ValidatedRequest.Client;
            var _roles = JsonConvert.SerializeObject(new List<string> { "anonymous" });

            var claims = new List<Claim> {
                new Claim("role", _roles),
                new Claim("typerole", "anonymous"),
            };
            claims.ToList().ForEach(u => context.Result.ValidatedRequest.ClientClaims.Add(u));
            context.Result.ValidatedRequest.Client.ClientClaimsPrefix = "";
        }
    }
}
