using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.SystemAdmin
{
    [Route("api/clubs")]
    [ApiController]
    public class ClubController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public ClubController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetClubs()
        {
            return Json(new { data = await _db.Clubs.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var clubFromDb = await _db.Clubs.FirstOrDefaultAsync(u => u.ClubId == id);
            if (clubFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.SystemUsers.Remove(_db.getClubRepresentativeAsUser(clubFromDb.ClubId).Result);
            _db.Clubs.Remove(clubFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete is Successful" });
        }
    }
}
