using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdminDashboard.StadiumsList
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public List<Stadium> Stadiums { get; set; }
        public async Task OnGet()
        {
            Stadiums = await _db.Stadia.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Stadium = await _db.Stadia.FindAsync(id);
            if (Stadium == null)
            {
                return NotFound();
            }
            _db.Stadia.Remove(Stadium);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
