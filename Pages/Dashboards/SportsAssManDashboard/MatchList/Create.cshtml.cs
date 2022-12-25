using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SportsAssManDashboard.MatchList
{
    public class CreateModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public CreateModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Match Match {get; set; }
        
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }
            await _db.Database.ExecuteSqlAsync($"exec addNewMatch {Match.GuestClub}, {Match.GuestClub}, {Match.StartTime},{Match.EndTime}");
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
