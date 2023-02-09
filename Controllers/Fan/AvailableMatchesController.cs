using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;

namespace Sports_Management_System.Controllers.Fan
{
    [Authorize(Roles = "Fan")]
	[Route("api/available-matches")]
    [ApiController]
    public class AvailableMatchesController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public AvailableMatchesController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableMatches()
        {
            return Json(new { data = await _db.GetAvailableMatches().ToListAsync() });
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseTicket(string hostClub, string guestClub, DateTime startTime)
        {
            string Username = Auth.GetCurrentUserName(User);
            Models.Fan fan = await _db.GetCurrentFan(Username);
            await _db.Database.ExecuteSqlAsync($"exec purchaseTicket {fan.NationalId}, {hostClub}, {guestClub}, {startTime}");
            return Json(new { success = true, message = "Ticket is Purchased Successfully" });
        }
    }
}
