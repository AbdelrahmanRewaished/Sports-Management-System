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
        public string ErrorMessage = "";

        public async Task OnGetAsync(string HostClub, string GuestClub, DateTime StartTime)
        {
            Match = await _db.Set<MatchView>().FirstOrDefaultAsync(
                u => u.HostClub! == HostClub &&
                u.GuestClub! == GuestClub &&
                u.StartTime! == StartTime
            );
            PreviousMatchId = await _db.GetMatchIdAsync(HostClub, GuestClub, StartTime);
        }

        private async Task<string> GetErrorMessage()
        {
            if (!ModelState.IsValid)
            {
                return "Fill All Fields Correctly";
            }
            if (Match.StartTime < DateTime.Now || Match.StartTime >= Match.EndTime)
            {
                return "Ending Time Must be later than Starting Time";
            }
            if (!await _db.IsClubExistingAsync(Match.HostClub!))
            {
                return "Host Club Does not exist";
            }
            if (!await _db.IsClubExistingAsync(Match.GuestClub!))
            {
                return "Guest Club Does not exist";
            }
            if (Match.HostClub == Match.GuestClub)
            {
                return "Host and Guest Cannot be the same Club";
            }
            return "";
        }

        public async Task<IActionResult> OnPost()
        {
            PreviousMatchId = int.Parse(Request.Form["id"]!);
            ErrorMessage = await GetErrorMessage();
            if (ErrorMessage != "")
            {
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec updateMatch {PreviousMatchId}, {Match.HostClub}, {Match.GuestClub}, {Match.StartTime}, {Match.EndTime}");
            return RedirectToPage("Index");
        }

    }
}
