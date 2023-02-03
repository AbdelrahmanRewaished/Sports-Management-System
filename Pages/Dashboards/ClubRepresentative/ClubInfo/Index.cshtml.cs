using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.ClubInfo
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public Club Club;
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
           
        }
        public async Task<IActionResult?> OnGet()
        {
            string path = ClubRepresentative.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }

            string Username = HttpContext.Session.GetString("Username")!;
            Club = await _db.Clubs.FindAsync((await _db.getCurrentClubRepresentative(Username)).ClubId);
            return null;
        }
    }
}
