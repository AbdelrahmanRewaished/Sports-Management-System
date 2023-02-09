using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
	[Authorize(Roles = "AssociationManager")]
	public class CreateModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public CreateModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UpComingMatch Match {get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Match.StartTime < DateTime.Now || Match.StartTime >= Match.EndTime)
            {
                return Page();
            }
            if(! _db.IsClubExisting(Match.HostClub!) || ! _db.IsClubExisting(Match.GuestClub!))
            {
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addNewMatch {Match.HostClub}, {Match.GuestClub}, {Match.StartTime}, {Match.EndTime}");
            return RedirectToPage("Index");
        }
    }
}
