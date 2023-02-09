using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;

namespace Sports_Management_System.Controllers.ClubRepresentative
{
    [Authorize(Roles = "ClubRepresentative")]
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
            Username = Auth.GetCurrentUserName(User);
            Models.ClubRepresentative representative = await _db.GetCurrentClubRepresentative(Username);

            return Json(new { data = await _db.GetAllHostRequestsSentBy(representative!.Id).ToListAsync()});
        }
    }
}
