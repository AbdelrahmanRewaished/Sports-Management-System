using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.ClubRepresentative
{
    [Route("api/sent-requests")]
    [ApiController]
    public class SentRequestsController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        private string Username;

        public SentRequestsController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetRequests()
        {
            Username = HttpContext.Session.GetString("Username")!;
            Models.ClubRepresentative representative = await _db.getCurrentClubRepresentative(Username);

            return Json(new { data = await _db.GetAllHostRequestsSentBy(representative!.Id).ToListAsync()});
        }
    }
}
