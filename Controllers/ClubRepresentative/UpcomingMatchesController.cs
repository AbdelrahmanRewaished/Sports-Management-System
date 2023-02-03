using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.ClubRepresentative
{
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
            Username = HttpContext.Session.GetString("Username")!;
            Club club = await _db.Clubs.FindAsync((await _db.getCurrentClubRepresentative(Username)!).ClubId);

            return Json(new { data = await _db.GetAllUpComingMatchesOfClub(club!.ClubId).ToListAsync()});
        }
    }
}
