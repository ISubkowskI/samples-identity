using Ae.Sample.Identity.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Ae.Sample.Identity.Authorization
{
    public class HrManagerProbationRequirementHandler : AuthorizationHandler<HrManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HrManagerProbationRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == AppClaimTypes.EmploymentDate))
            {
                return Task.CompletedTask;
            }

            if (DateTimeOffset.TryParse(context.User.FindFirst(c => c.Type == AppClaimTypes.EmploymentDate)?.Value, out DateTimeOffset employmentDate))
            {
                var period = DateTimeOffset.Now - employmentDate;
                if (period.Days > 30 * requirement.ProbationMonths)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
