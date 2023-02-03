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
			if (registeringStadiumManager.Password.Length < 6)
			{
				errorMessage = "Password must be longer than 5 characters";
				return Page();
			}
			if (registeringStadiumManager.Password.Length > 20)
			{
				errorMessage = "Password must be shorter than 20 characters";
				return Page();
			}
			if (!registeringStadiumManager.Password.Equals(registeringStadiumManager.ConfirmPassword))
			{
				errorMessage = "Passwords must match";
				return Page();
			}

			SystemUser user = await _db.SystemUsers.FindAsync(registeringStadiumManager.Username);
            if (user != null)
            {
                errorMessage = "Already registered";
                return Page( );
            }
            var stadiums = _db.Stadia
                .Where(n => n.Name == registeringStadiumManager.Entity)
                .ToList();

            if(stadiums.Count == 0)
            {
                errorMessage = "Stadium doesn't exist";
                return Page();
            }
            var stadiumManagers = _db.Database
                .SqlQuery<string>($"SELECT * FROM AllStadiumManagers WHERE stadium_name = {registeringStadiumManager.Entity}")
                .ToList();

            if (stadiumManagers.Count != 0)
            {
                errorMessage = "A Stadium Manager already exists ";
                return Page();
            }

            await _db.Database.ExecuteSqlAsync($"exec addStadiumManager {registeringStadiumManager.Name}, {registeringStadiumManager.Entity} ,{registeringStadiumManager.Username}, {registeringStadiumManager.Password}");
            return LogUserIn(registeringStadiumManager.Username, "StadiumManager");
        }
    }
    
}
