using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Controllers.SystemAdmin
{
	[Authorize(Roles = "SystemAdmin")]
	[Route("api/fans")]
    [ApiController]
    public class FanController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public FanController(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetFans()
        {
            return Json(new { data = await _db.Fans.ToListAsync() });
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateStatus(string nationalId, bool status)
        {
            var fanFromDb = await _db.Fans.FirstOrDefaultAsync(u => u.NationalId == nationalId);
            if (fanFromDb == null)
            {
                return Json(new { success = false, message = "Error while Updating Status" });
            }
            if(status)
            {
                await _db.Database.ExecuteSqlAsync($"exec blockFan {nationalId}");
            }
            else
            {
                await _db.Database.ExecuteSqlAsync($"exec unblockFan {nationalId}");
            }
            return Json(new { success = true, message = "Status Update is Successful" });
        }
    }
}
