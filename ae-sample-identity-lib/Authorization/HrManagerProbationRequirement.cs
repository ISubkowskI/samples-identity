using Microsoft.AspNetCore.Authorization;

namespace Ae.Sample.Identity.Authorization
{
    public class HrManagerProbationRequirement : IAuthorizationRequirement
    {
        public int ProbationMonths { get; }

        public HrManagerProbationRequirement(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }
    }
}
