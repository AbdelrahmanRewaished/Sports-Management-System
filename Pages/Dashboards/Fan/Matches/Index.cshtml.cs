using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan.Matches
{
    public class IndexModel : PageModel
    {
        public IActionResult? OnGet()
        {
            string path = Fan.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }
            return null;

        }
    }
}
