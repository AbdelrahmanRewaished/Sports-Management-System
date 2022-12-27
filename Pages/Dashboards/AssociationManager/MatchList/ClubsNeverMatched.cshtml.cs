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
        
        public void OnGet()
        {
        }
    }
}
