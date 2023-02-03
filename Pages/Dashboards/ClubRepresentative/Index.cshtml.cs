using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Microsoft.AspNetCore.Http;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
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
            return Auth.Login.Auth.getRedirectionPath(httpContext, "ClubRepresentative");
        }
    }
}
