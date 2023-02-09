using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.SystemAdmin
{
	[Authorize(Roles = "SystemAdmin")]
	[Route("api/stadiums")]
    [ApiController]
    public class StadiumController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public StadiumController(ChampionsLeagueDbContext db)
        {
            _db = db;   
        }
        [HttpGet]
        public async Task<IActionResult> GetStadiums()
        {
            return Json(new { data = await _db.Stadia.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var stadiumFromDb = await _db.Stadia.FirstOrDefaultAsync(u => u.Id == id);
            if (stadiumFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            var StadiumManager = await _db.GetStadiumManagerAsUser(stadiumFromDb.Name!);
            if(StadiumManager != null)
            {
                _db.SystemUsers.Remove(StadiumManager);
            }
            _db.Stadia.Remove(stadiumFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete is Successful" });
        }
    }
}
