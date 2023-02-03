using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult?> OnGet()
        {
            string path = AssociationManager.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }
            return null;
        }
    }
}
