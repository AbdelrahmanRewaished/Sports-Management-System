using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager
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
            if (Role != "AssociationManager")
            {
                return Redirect("../../Auth/UnAuthorized");
            }
            return null;
        }
    }
}
