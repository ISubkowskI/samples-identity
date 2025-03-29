using Microsoft.AspNetCore.Authorization;

namespace Ae.Sample.Identity.Authorization
{
    public class HrManagerProbationRequirementHandler : AuthorizationHandler<HrManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HrManagerProbationRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }

            if (DateTime.TryParse(context.User.FindFirst(c => c.Type == "EmploymentDate")?.Value, out DateTime employmentDate))
            {
                var period = DateTime.Now - employmentDate;
                if (period.Days > 30 * requirement.ProbationMonths)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
