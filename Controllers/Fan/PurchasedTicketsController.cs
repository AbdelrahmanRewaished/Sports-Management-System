using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;

namespace Sports_Management_System.Controllers.Fan
{
    [Authorize(Roles = "Fan")]
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
            string Username = Auth.GetCurrentUserName(User);
            Models.Fan fan = await _db.GetCurrentFan(Username);
            return Json(new { data = await _db.ViewPurchasedTickets(fan.NationalId).ToListAsync()});
        }
    }
}
