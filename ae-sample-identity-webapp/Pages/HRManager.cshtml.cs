using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ae.Sample.Identity.Pages
{
    [Authorize(Policy = "HRManagerOnly")]
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
