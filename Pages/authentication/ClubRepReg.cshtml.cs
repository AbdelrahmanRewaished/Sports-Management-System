using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.authentication
{
    public class ClubRepRegModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

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
                return Page();
            }
            SystemUser user = await _db.SystemUsers.FindAsync(registeringClubRepresentative.Username);
            if (user != null)
            {
                return Page();
            }
            var clubs = _db.Clubs
                .FromSql($"SELECT * FROM Club")
                .Where(n => n.Name == registeringClubRepresentative.Entity)
                .ToList();

            if (clubs.Count == 0)
            {
                return Page();
            }
            var clubRepresentatives = _db.Database
                .SqlQuery<string>($"SELECT * FROM AllClubRepresentatives WHERE club_name = {registeringClubRepresentative.Entity}")
                .ToList();

            if (clubRepresentatives.Count != 0)
            {
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addRepresentative {registeringClubRepresentative.Name}, {registeringClubRepresentative.Entity}, {registeringClubRepresentative.Username}, {registeringClubRepresentative.Password}");
            return RedirectToPage("Login");
        }
    }
}
