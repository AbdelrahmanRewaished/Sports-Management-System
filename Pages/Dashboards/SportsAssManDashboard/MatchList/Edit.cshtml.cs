using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Sports_Management_System.Pages.Dashboards.SportsAssManDashboard.MatchList
{
    public class EditModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public EditModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Match Match { get; set; }
        public async Task OnGet( int Id)
        {
            Match = await  _db.Matches.FindAsync(Id);

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var MatchFromDb = await _db.Matches.FindAsync(Match.MatchId);
            MatchFromDb.HostClub=Match.HostClub;
            MatchFromDb.GuestClub=Match.GuestClub;
            MatchFromDb.StartTime= Match.StartTime;
            MatchFromDb.EndTime= Match.EndTime;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }

    }
}
