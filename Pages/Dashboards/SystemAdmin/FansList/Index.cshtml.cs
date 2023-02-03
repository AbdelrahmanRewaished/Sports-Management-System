using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.FansList
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult?> OnGet()
        {
            string path = SystemAdmin.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }
            return null;
        }
    }
}
