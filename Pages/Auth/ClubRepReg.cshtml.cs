using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Auth
{
    public class ClubRepRegModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string errorMessage = "";

        public ClubRepRegModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public StadManager_ClubRepWrapper registeringClubRepresentative { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (! ModelState.IsValid || !registeringClubRepresentative.Password.Equals(registeringClubRepresentative.ConfirmPassword))
            {
                errorMessage = "Passwords must match";
                return Page();
            }
            SystemUser user = await _db.SystemUsers.FindAsync(registeringClubRepresentative.Username);
            if (user != null)
            {
                errorMessage = "Already registered";
                return Page();
            }
            var clubs = _db.Clubs
                .FromSql($"SELECT * FROM Club")
                .Where(n => n.Name == registeringClubRepresentative.Entity)
                .ToList();

            if (clubs.Count == 0)
            {
                errorMessage = "Club doesn't exist";
                return Page();
            }
            var clubRepresentatives = _db.Database
                .SqlQuery<string>($"SELECT * FROM AllClubRepresentatives WHERE club_name = {registeringClubRepresentative.Entity}")
                .ToList();

            if (clubRepresentatives.Count != 0)
            {
                errorMessage = "A Club Representative  already exists ";
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addRepresentative {registeringClubRepresentative.Name}, {registeringClubRepresentative.Entity}, {registeringClubRepresentative.Username}, {registeringClubRepresentative.Password}");
            return RedirectToPage("Login");
        }
    }
}
