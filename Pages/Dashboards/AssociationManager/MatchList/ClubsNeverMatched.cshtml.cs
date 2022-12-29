using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class ClubsNeverMatchedModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public List<ClubsNeverMatched> ClubsNeverMatched;
        public ClubsNeverMatchedModel(ChampionsLeagueDbContext db)
        {
            _db = db;
            ClubsNeverMatched = _db.ClubsNeverMatcheds.ToList();
        }

        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "AssociationManager")
            {
                return Redirect("../../../../Auth/UnAuthorized");
            }
            return null;
        }
    }
}
