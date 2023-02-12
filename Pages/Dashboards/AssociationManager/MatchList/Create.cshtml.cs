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
        public string ErrorMessage = "";
        public CreateModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UpComingMatch Match {get; set; }

        private async Task<string> GetErrorMessage()
        {
            if (!ModelState.IsValid)
            {
                return "Fill All Fields Correctly";
            }
            if(Match.StartTime >= Match.EndTime)
            {
                 return "Ending Time Must be later than Starting Time";
            }
            if (Match.StartTime < DateTime.Now)
            {
                return "Time of the match must be in the future";
            }
            if (! await _db.IsClubExistingAsync(Match.HostClub!))
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
            ErrorMessage = await GetErrorMessage();
            if(ErrorMessage != "")
            {
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addNewMatch {Match.HostClub}, {Match.GuestClub}, {Match.StartTime}, {Match.EndTime}");
            return RedirectToPage("Index");
        }
    }
}
