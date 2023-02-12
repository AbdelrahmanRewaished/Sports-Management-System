using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.Auth
{
    public class StadiumManagerRegModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string errorMessage = "";
        public StadiumManagerRegModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public StadManager_ClubRepWrapper registeringStadiumManager { get; set; }
        private IActionResult LogUserIn()
        {
            Auth.SetUserClaims(HttpContext, registeringStadiumManager.Username, Auth.StadiumManagerRole);
            string destination = Auth.GetLoggingUserDestination(Auth.StadiumManagerRole);
            return Redirect(destination);
        }

        private async Task<bool> StadiumHasAlreadyAManager()
        {
            return await _db.GetStadiumManager(registeringStadiumManager.Entity) != null;
        }

        private async Task<string> GetErrorMessage()
        {
			if (!ModelState.IsValid)
			{
				return "Fill All Fields Correctly";
			}
			string errorMessage = Auth.GetPasswordErrorMessage(registeringStadiumManager.Password, registeringStadiumManager.ConfirmPassword);
			if (errorMessage != "")
			{
                return errorMessage;
			}
			if (await Auth.IsUserExisting(_db, registeringStadiumManager.Username))
			{
				return "Already registered";
			}
			if (! await _db.IsStadiumExisting(registeringStadiumManager.Entity))
			{
				return "Stadium doesn't exist";
			}

			if (await StadiumHasAlreadyAManager())
			{
				return "A Stadium Manager already exists ";
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
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registeringStadiumManager.Password);
            await _db.Database.ExecuteSqlAsync($"exec addStadiumManager {registeringStadiumManager.Name}, {registeringStadiumManager.Entity} ,{registeringStadiumManager.Username}, {hashedPassword}");
            return LogUserIn();
        }
    }
    
}
