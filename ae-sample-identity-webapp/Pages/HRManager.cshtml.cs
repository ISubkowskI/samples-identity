using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ae.Sample.Identity.Authorization;

namespace Ae.Sample.Identity.Pages
{
    [Authorize(Policy = AppPolicyNames.HRManagerOnly)]
    public class HRManagerModel : PageModel
    {
        private readonly ILogger<HRManagerModel> _logger;

        public HRManagerModel(ILogger<HRManagerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
