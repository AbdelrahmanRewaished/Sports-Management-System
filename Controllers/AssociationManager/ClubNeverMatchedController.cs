using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.AssociationManager
{
    [Route("api/clubs-never-matched")]
    public class ClubNeverMatchedController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public ClubNeverMatchedController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return Json(new { data = await _db.Set<ClubsNeverMatched>().ToListAsync() });
        }
    }
}
