using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.AssociationManager
{
    [Route("api/upcoming-matches")]
    [ApiController]
    public class UpcomingMatchesController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public UpcomingMatchesController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetMatchesAsync()
        {
            return Json(new {data = await _db.Set<UpComingMatch>().ToListAsync()});
        }
    }

   
}
