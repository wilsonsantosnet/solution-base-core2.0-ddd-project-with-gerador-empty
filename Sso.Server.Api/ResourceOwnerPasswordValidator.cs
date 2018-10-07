using Common.Domain.Base;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.Options;
using Sso.Server.Api.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sso.Server.Api
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        
        private readonly ConfigSettingsBase _settings;
        private readonly IUserCredentialServices _userServices;

        public ResourceOwnerPasswordValidator(IOptions<ConfigSettingsBase> configSettingsBase, IUserCredentialServices userServices)
        {
            this._settings = configSettingsBase.Value;
            this._userServices = userServices;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {

                var userServices = this._userServices;
                
                var user = await userServices.Auth(context.UserName, context.Password);

                if (user.IsNotNull())
                {
                    context.Result = new GrantValidationResult(
                        subject: user.SubjectId,
                        authenticationMethod: "custom",
                        claims: user.Claims);

                    return;
                }

                context.Result = new GrantValidationResult(
                            TokenRequestErrors.InvalidGrant,
                            "invalid custom credential");

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
