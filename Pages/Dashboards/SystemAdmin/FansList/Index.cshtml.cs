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
        public async Task OnGet()
        {
            Fans = await _db.Fans.ToListAsync();
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
