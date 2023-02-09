using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Auth
{
    public class ClubRepRegModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
       
        public ClubRepRegModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
		
		[BindProperty]
        public StadManager_ClubRepWrapper registeringRepresentative { get; set; }
        public string errorMessage = "";

        private async Task<bool> IsClubExisting()
        {
			var club = await _db.Clubs.FirstOrDefaultAsync(u => u.Name == registeringRepresentative.Entity);
            return club != null;
		}
        private async Task<bool> ClubHasAlreadyARepresentative()
        {
			var clubRepresentative = await _db.ClubRepresentatives
			   .FirstOrDefaultAsync(u => u.ClubId == _db.GetClubId(registeringRepresentative.Entity).Result);
            return clubRepresentative != null;
		}
		private IActionResult LogUserIn()
		{
			Auth.SetUserClaims(HttpContext, registeringRepresentative.Username, Auth.RepresentativeRole);
			string destination = Auth.GetLoggingUserDestination(Auth.RepresentativeRole);
			return Redirect(destination);
		}

        private async Task<string> GetErrorMessage()
        {
			if (!ModelState.IsValid)
			{
				return "Fill All Fields Correctly";
			}
			string errorMessage = Auth.GetPasswordErrorMessage(registeringRepresentative.Password, registeringRepresentative.ConfirmPassword);
			if(errorMessage != "")
			{
				return errorMessage;
			}
			if (await Auth.IsUserExisting(_db, registeringRepresentative.Username))
			{
				return "Already registered";
			}
			if (! await IsClubExisting())
			{
				return "Club doesn't exist";
			}
			if (await ClubHasAlreadyARepresentative())
			{
				return "A Club Representative  already exists ";
			}
			return "";
		}
		public async Task<IActionResult> OnPost()
        {
			errorMessage = await GetErrorMessage();
			if(errorMessage != "")
			{
				return Page();
			}
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registeringRepresentative.Password);
			await _db.Database.ExecuteSqlAsync($"exec addRepresentative {registeringRepresentative.Name}, {registeringRepresentative.Entity}, {registeringRepresentative.Username}, {hashedPassword}");
            return LogUserIn();
        }
      
    }
}
