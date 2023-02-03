/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.ClubRepresentative
{
    [Route("api/available-stadiums")]
    [ApiController]
    public class AvailableStadiumsController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;

        public AvailableStadiumsController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetStadiums(string hostClub, string guestClub, DateTime startTime)
        {
            HttpContext.Session.SetString("MatchStartTime", startTime.ToString());
            return Json(new { date = await _db.ViewAvailableStadiumsOn(hostClub, guestClub, startTime).ToListAsync() });
        }

        [HttpPost]
        public async Task<IActionResult> SendRequest(string Stadium)
        {
            string Username = HttpContext.Session.GetString("Username")!;
            Club club = await _db.Clubs.FindAsync((await _db.getCurrentClubRepresentative(Username)).ClubId);
            string clubName = club!.Name!;
            string MatchStartTime = HttpContext.Session.GetString("MatchStartTime")!;
            await _db.Database.ExecuteSqlAsync($"exec AddHostRequest {clubName}, {Stadium}, {MatchStartTime}");

           *//* HttpContext.Session.Remove("HostClub");
            HttpContext.Session.Remove("GuestClub");*//*
            HttpContext.Session.Remove("MatchStartTime");
            return Redirect("ClubInfo");
        }
    }
}
*/