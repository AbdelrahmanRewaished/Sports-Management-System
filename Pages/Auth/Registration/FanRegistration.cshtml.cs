using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.Auth
{

    public class FanRegistrationModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string errorMessage = "";
        public FanRegistrationModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public class FanRegWrapper
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            [DataType(DataType.Password), Compare(nameof(Password))]
            public string ConfirmPassword { get; set; }

            public string NationalId { get; set; }

            public string Phone { get; set; }

            public DateTime Birthdate { get; set; }

            public string Address { get; set; }
        }

        [BindProperty]
        public FanRegWrapper registeringFan { get; set; }
        private IActionResult LogUserIn(string username, string role)
        {
            Login.Auth.setUserSession(HttpContext, username, role);
            string destination = Login.Auth.getLoggedUserDestination(role);
            return Redirect(destination);
        }

        private bool isRegisterUnderAge(DateTime birthdate)
        {
            int currentYear = DateTime.Now.Year;
            int registerBirthYear = birthdate.Year;
            return currentYear - registerBirthYear < 16;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) 
            {
                errorMessage = "Fill All Fields Correctly";
                return Page();
            }
			if (registeringFan.Password.Length < 6)
			{
				errorMessage = "Password must be longer than 5 characters";
				return Page();
			}
			if (registeringFan.Password.Length > 20)
			{
				errorMessage = "Password must be shorter than 20 characters";
				return Page();
			}
			if (!registeringFan.Password.Equals(registeringFan.ConfirmPassword))
            {
				errorMessage = "Passwords must match";
				return Page();
			}
			if(isRegisterUnderAge(registeringFan.Birthdate)) 
            {
                errorMessage = "A Fan must be atleast 16 years of age";
                return Page();
            }

			SystemUser user = await _db.SystemUsers.FindAsync(registeringFan.Username);
            if (user != null)
            {
                errorMessage = "Already registered";
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addFan {registeringFan.Name}, {registeringFan.Username} ,{registeringFan.Password}, {registeringFan.NationalId}, {registeringFan.Birthdate}, {registeringFan.Address}, {registeringFan.Phone}");
            return LogUserIn(registeringFan.Username, "Fan");
        }
    }
}
