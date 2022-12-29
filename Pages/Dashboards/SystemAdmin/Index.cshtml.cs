using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin
{
    public class IndexModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "SystemAdmin")
            {
                return Redirect("../../Auth/UnAuthorized");
            }
            return null;
        }
    }
}
