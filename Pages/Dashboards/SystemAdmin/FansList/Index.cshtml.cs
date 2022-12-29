using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.FansList
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public List<Models.Fan> Fans { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "SystemAdmin")
            {
                return Redirect("../../Auth/UnAuthorized");
            }
            Fans = await _db.Fans.ToListAsync();
            return null;
        }

        public async Task<IActionResult> OnPost(int id, bool status)
        {
            var Fan = await _db.Fans.FindAsync(id);
            if (Fan == null)
            {
                return NotFound();
            }
            if(status)
            {
                await _db.Database.ExecuteSqlAsync($"exec blockFan {id}");
            }
            else
            {
                await _db.Database.ExecuteSqlAsync($"exec unblockFan {id}");
            }
            return RedirectToPage("Index");
        }
    }
}
