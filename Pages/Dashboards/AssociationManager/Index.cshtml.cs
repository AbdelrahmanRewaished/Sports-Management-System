using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager
{
    public class IndexModel : PageModel
    {
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
            return Auth.Login.Auth.getRedirectionPath(httpContext, "AssociationManager");
        }
    }
}
