using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin
{
    [Authorize(Roles = "SystemAdmin")]
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public string ClubsCount { get; set; }
        public string StadiumsCount { get; set; }
        public string FansCount { get; set; }
        public async Task OnGetAsync()
        {
            ClubsCount = NumberFormatter.getFormattedNumber(await _db.GetClubsCount());
            FansCount = NumberFormatter.getFormattedNumber(await _db.GetFansCount());
            StadiumsCount = NumberFormatter.getFormattedNumber(await _db.GetStadiumsCount());
        }
       
    }
}
