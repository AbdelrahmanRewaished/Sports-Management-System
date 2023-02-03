using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.Fan
{
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
            string Username = HttpContext.Session.GetString("Username")!;
            Models.Fan fan = _db.getCurrentFan(Username);
            await _db.Database.ExecuteSqlAsync($"exec purchaseTicket {fan.NationalId}, {hostClub}, {guestClub}, {startTime}");
            return Json(new { success = true, message = "Ticket is Purchased Successfully" });
        }
    }
}
