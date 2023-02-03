using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager
{
    public class IndexModel : PageModel
    {
        public string Username { get; set; }
        
        public IActionResult? OnGet()
        {
            string path = getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }
            return null;
        }
        public static string getRedirectionPath(HttpContext httpContext)
        {
            return Auth.Login.Auth.getRedirectionPath(httpContext, "StadiumManager");
        }
    }
}
