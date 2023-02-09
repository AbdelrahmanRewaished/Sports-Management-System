using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
	[Authorize(Roles = "AssociationManager")]
	public class EditModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public EditModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public MatchView Match { get; set; }
        public int PreviousMatchId { get; set; }

        public async Task OnGetAsync(string HostClub, string GuestClub, DateTime StartTime)
        {
            Match = await _db.Set<MatchView>().FirstOrDefaultAsync(
                u => u.HostClub! == HostClub &&
                u.GuestClub! == GuestClub &&
                u.StartTime! == StartTime
            );
            PreviousMatchId = await _db.GetMatchIdAsync(HostClub, GuestClub, StartTime);
        }

        public async Task<IActionResult> OnPost()
        {
            PreviousMatchId = Int32.Parse(Request.Form["id"]!);
            if(! ModelState.IsValid)
            {
                return Page();
            }
            if (Match.StartTime < DateTime.Now || Match.StartTime >= Match.EndTime)
            {
                return Page();
            }
            if (!_db.IsClubExisting(Match!.HostClub!) || !_db.IsClubExisting(Match!.GuestClub!))
            {
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec updateMatch {PreviousMatchId}, {Match.HostClub}, {Match.GuestClub}, {Match.StartTime}, {Match.EndTime}");
            return RedirectToPage("Index");
        }

    }
}
