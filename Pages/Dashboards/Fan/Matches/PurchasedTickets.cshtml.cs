using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan.Matches
{
    public class PurchasedTicketsModel : PageModel
    {
        public IActionResult? OnGet()
        {
            string Username = HttpContext.Session.GetString("Username")!;
            string path = Fan.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }
            return null;
        }
    }
}
