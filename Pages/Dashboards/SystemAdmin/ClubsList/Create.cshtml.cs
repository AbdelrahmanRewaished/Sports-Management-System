using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.ClubsList
{
    [Authorize(Roles = "SystemAdmin")]
    public class CreateModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string ErrorMessage = "";
        public CreateModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Club Club { get; set; }

        private async Task<string> GetErrorMessage()
        {
            if (!ModelState.IsValid)
            {
                return "Fill All Fields Correctly";
            }
            if(await _db.IsClubExistingAsync(Club.Name!))
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
            await _db.Clubs.AddAsync(Club);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
