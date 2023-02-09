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
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public string PendingRequestsCount { get; set; }
        public async Task OnGetAsync()
        {
            string Username = Auth.Auth.GetCurrentUserName(User);
            PendingRequestsCount = NumberFormatter.getFormattedNumber(await _db.GetTotalPendingRequests(Username));
        }
    }
}
