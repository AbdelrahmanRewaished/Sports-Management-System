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

        private IActionResult LogUserIn(string username, string role)
        {
            Login.Auth.setUserSession(HttpContext, username, role);
            string destination = Login.Auth.getLoggedUserDestination(role);
            return Redirect(destination);
        }
        public async Task<IActionResult> OnPost()
        {
            if (! ModelState.IsValid) 
            {
                errorMessage = "Fill All Fields Correctly";
                return Page();
            }
            if(registeringClubRepresentative.Password.Length < 6)
            {
				errorMessage = "Password must be longer than 5 characters";
				return Page();
			}
			if (registeringClubRepresentative.Password.Length > 20)
			{
				errorMessage = "Password must be shorter than 20 characters";
				return Page();
			}
			if (! registeringClubRepresentative.Password.Equals(registeringClubRepresentative.ConfirmPassword))
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

            return LogUserIn(registeringClubRepresentative.Username, "ClubRepresentative");
        }
    }
}