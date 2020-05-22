using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Common.API.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services, Func<IEnumerable<Claim>, IDictionary<string, object>> defineProfile)
        {

            return services.AddAuthorization(options =>
            {
                options.AddPolicy(name: "CanReadOne", configurePolicy: policy => policy.RequireAssertion((AuthorizationHandlerContext e) =>
                {
                    return e.VerifyClaimsCanReadOne(defineProfile(e.User.Claims).GetTools());
                }));

                options.AddPolicy(name: "CanReadAll", configurePolicy: policy => policy.RequireAssertion((AuthorizationHandlerContext e) =>
                {
                    return e.VerifyClaimsCanReadAll(defineProfile(e.User.Claims).GetTools());
                }));

                options.AddPolicy(name: "CanEdit", configurePolicy: policy => policy.RequireAssertion(e =>
                {
                    return e.VerifyClaimsCanEdit(defineProfile(e.User.Claims).GetTools());
                }));

                options.AddPolicy(name: "CanSave", configurePolicy: policy => policy.RequireAssertion(e =>
                {
                    return e.VerifyClaimsCanSave(defineProfile(e.User.Claims).GetTools());
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
