using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.ClubsList
{
	[Authorize(Roles = "SystemAdmin")]
	public class EditModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string ErrorMessage = "";
        public EditModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Club Club { get; set; }
        public async Task OnGet(int id)
        {
            Club = await _db.Clubs.FindAsync(id);
        }
        private async Task<string> GetErrorMessage()
        {
            if (!ModelState.IsValid)
            {
                return "Fill All Fields Correctly";
            }
            if (await _db.IsClubExistingAsync(Club.Name!))
            {
                return "Club Already Exists";
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
            var ClubFromDb = await _db.Clubs.FindAsync(Club.ClubId);
            ClubFromDb.Name = Club.Name;
            ClubFromDb.Location = Club.Location;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
