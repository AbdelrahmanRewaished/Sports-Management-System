using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;
using System.Security.Claims;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.ClubInfo
{
    [Authorize(Roles = "ClubRepresentative")]
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public Club Club;
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public async Task OnGet()
        {
            string Username = Auth.Auth.GetCurrentUserName(User); ;
            Club = await _db.Clubs.FindAsync((await _db.GetCurrentClubRepresentative(Username)).ClubId);
        }
    }
}
