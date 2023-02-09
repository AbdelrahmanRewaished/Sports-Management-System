using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;

namespace Sports_Management_System.Controllers.ClubRepresentative
{
    [Authorize(Roles = "ClubRepresentative")]
	[Route("api/club-upcoming-matches")]
    [ApiController]
    public class UpcomingMatchesController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        private string Username;

        public UpcomingMatchesController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            Username = Auth.GetCurrentUserName(User);
			Club club = await _db.Clubs.FindAsync((await _db.GetCurrentClubRepresentative(Username)!).ClubId);
            return Json(new { data = await _db.GetAllUpComingMatchesOfClub(club!.ClubId).ToListAsync()});
        }
    }
}
