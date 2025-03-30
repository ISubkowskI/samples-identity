namespace Ae.Sample.Identity.Authorization
{
    public static class AppPolicyNames
    {
        public const string AdminOnly = "AdminOnly";
        public const string HRManagerOnly = "HRManagerOnly";
        public const string MustBelongToHRDepartment = "MustBelongToHRDepartment";
    }
}
