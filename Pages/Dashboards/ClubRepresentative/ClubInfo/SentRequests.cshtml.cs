using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.ClubInfo
{
    public class SentRequestsModel : PageModel
    {
        public IActionResult? OnGet()
        {
            string path = ClubRepresentative.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }
            return null;

        }
    }
}