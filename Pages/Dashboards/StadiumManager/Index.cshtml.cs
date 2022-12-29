using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string Username { get; set; }
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username")!;
            if (Username == null)
            {
                return Redirect("../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role")!;
            if (Role != "StadiumManager")
            {
                return Redirect("../../Auth/UnAuthorized");
            }
            return null;
        }
    }
}
