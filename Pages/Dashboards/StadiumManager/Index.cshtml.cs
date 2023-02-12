using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager
{
	[Authorize(Roles = "StadiumManager")]
	public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public Stadium Stadium;
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public string PendingRequestsCount { get; set; }
        public async Task OnGetAsync()
        {
            string Username = Auth.Auth.GetCurrentUserName(User);
            Stadium = await _db.Stadia.FindAsync((await _db.GetCurrentStadiumManager(Username)!).StadiumId);
            PendingRequestsCount = NumberFormatter.getFormattedNumber(await _db.GetTotalPendingRequests(Username));
        }
    }
}
