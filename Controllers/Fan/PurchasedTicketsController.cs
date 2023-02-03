using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.Fan
{
    [Route("api/purchased-tickets")]
    [ApiController]
    public class PurchasedTicketsController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public PurchasedTicketsController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchasedTickets()
        {
            string Username = HttpContext.Session.GetString("Username")!;
            Models.Fan fan = _db.getCurrentFan(Username);
            return Json(new { data = await _db.ViewPurchasedTickets(fan.NationalId).ToListAsync()});
        }
    }
}
