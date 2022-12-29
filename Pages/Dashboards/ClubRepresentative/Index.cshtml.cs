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

        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "ClubRepresentative")
            {
                return Redirect("../../Auth/UnAuthorized");
            }
            return null;
        }
        
    }
}
