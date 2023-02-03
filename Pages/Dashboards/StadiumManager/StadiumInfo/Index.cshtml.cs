using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager.StadiumInfo
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public Stadium Stadium;
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db; 
        }
        public async Task<IActionResult?> OnGet()
        {
            string path = StadiumManager.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }

            string Username = HttpContext.Session.GetString("Username")!;
            Stadium = await _db.Stadia.FindAsync((await _db.getCurrentStadiumManager(Username)!).StadiumId);
            return null;
        }
    }
}
