using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;
using System.Security.Claims;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager.StadiumInfo
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
        public async Task OnGet()
        {
            string Username = Auth.Auth.GetCurrentUserName(User);
			Stadium = await _db.Stadia.FindAsync((await _db.GetCurrentStadiumManager(Username)!).StadiumId);
        }
    }
}
