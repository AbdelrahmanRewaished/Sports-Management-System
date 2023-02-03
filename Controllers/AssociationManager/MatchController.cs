using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.AssociationManager
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public MatchController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            return Json(new { data = await _db.AllMatches.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string HostClub, string GuestClub, DateTime StartTime)
        {
            var match = await _db.Matches.FindAsync(_db.getMatchIdAsync(HostClub, GuestClub, StartTime));
            if (match == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Matches.Remove(match);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete is Successful" });
        }
    }
}
