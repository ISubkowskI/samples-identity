using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ae.Sample.Identity.Authorization;

namespace Ae.Sample.Identity.Pages
{
    [Authorize(Policy = AppPolicyNames.MustBelongToHRDepartment)]
    public class HumanResourceModel : PageModel
    {
        private readonly ILogger<HumanResourceModel> _logger;

        public HumanResourceModel(ILogger<HumanResourceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
