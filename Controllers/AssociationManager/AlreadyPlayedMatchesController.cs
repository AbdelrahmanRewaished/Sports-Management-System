using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.AssociationManager
{
    [Route("api/already-played-matches")]
    [ApiController]
    public class AlreadyPlayedMatchesController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public AlreadyPlayedMatchesController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetMatchesAsync()
        {
            return Json(new { data = await _db.Set<AlreadyPlayedMatch>().ToListAsync() });
        }
    }
}
