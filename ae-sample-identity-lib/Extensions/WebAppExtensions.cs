using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Ae.Sample.Identity.Authorization;

namespace Ae.Sample.Identity.Extensions
{
    public static class WebAppExtensions
    {
        public static IServiceCollection AddWebAppAuthentication(this IServiceCollection services, string authenticationScheme, Action<CookieAuthenticationOptions> cookieConfigureOptions)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddAuthentication(authenticationScheme)
                    .AddCookie(authenticationScheme, cookieConfigureOptions);

            return services;
        }

        public static IServiceCollection AddWebAppAuthorization(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(AppPolicyNames.AdminOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(AppClaimTypes.Admin);
                });

                options.AddPolicy(AppPolicyNames.HRManagerOnly, policy => policy
                    .RequireAuthenticatedUser()
                    .RequireClaim(AppClaimTypes.Department, "HR")
                    .RequireClaim(AppClaimTypes.Manager)
                    .Requirements.Add(new HrManagerProbationRequirement(probationMonths:3)));

                options.AddPolicy(AppPolicyNames.MustBelongToHRDepartment, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(AppClaimTypes.Department, "HR");
                });
            });

            services.AddSingleton<IAuthorizationHandler, HrManagerProbationRequirementHandler>();

            return services;
        }
    }
}
