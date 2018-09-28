using Common.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Common.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services, Func<IEnumerable<Claim>, IDictionary<string, object>> defineProfile) {

            return services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "CanRead", configurePolicy: policy => policy.RequireAssertion((AuthorizationHandlerContext e) =>
                {
                    return e.VerifyClaimsCanRead(defineProfile(e.User.Claims).GetTools());
                }));

                options.AddPolicy(name: "CanWrite", configurePolicy: policy => policy.RequireAssertion(e =>
                {
                    return e.VerifyClaimsCanWrite(defineProfile(e.User.Claims).GetTools());
                }));

                options.AddPolicy(name: "CanDelete", configurePolicy: policy => policy.RequireAssertion(e =>
                {
                    return e.VerifyClaimsCanDelete(defineProfile(e.User.Claims).GetTools());
                }));
            });
        }

    }
}
