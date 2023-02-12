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
            [Required]
            public string NationalId { get; set; }
            [Required]
            public string Phone { get; set; }
            [Required]
            public DateOnly Birthdate { get; set; }
            [Required]
            public string Address { get; set; }
        }

        [BindProperty]
        public FanRegWrapper registeringFan { get; set; }
        private async Task<IActionResult> LogUserIn()
        {
            await Auth.SetUserClaims(HttpContext, registeringFan.Username, Auth.FanRole);
            string destination = Auth.GetLoggingUserDestination(Auth.FanRole);
            return Redirect(destination);
        }

        private bool IsRegisterUnderAge()
        {
            int currentYear = DateTime.Now.Year;
            int registerBirthYear = registeringFan.Birthdate.Year;
            return currentYear - registerBirthYear < 16;
        }

        private async Task<string> GetErrorMessage()
        {
			if (!ModelState.IsValid)
			{
				return "Fill All Fields Correctly";
			}
			string errorMessage = Auth.GetPasswordErrorMessage(registeringFan.Password, registeringFan.ConfirmPassword);
			if (errorMessage != "")
			{
                return errorMessage;
			}
			if (IsRegisterUnderAge())
			{
				return "A Fan must be atleast 16 years of age";
			}
			if (await Auth.IsUserExisting(_db, registeringFan.Username))
			{
				return "Already registered";
			}
            Fan fan = await _db.Fans.FindAsync(registeringFan.NationalId);
            if(fan != null)
            {
                return "This NationalId Belongs to someone else. Insert your correct NationalId !";
            }
            return "";
		}

        private string GetSQLDateFormat(DateOnly date)
        {
            return date.Year + "-" + date.Month + "-" + date.Day;
        }

        public async Task<IActionResult> OnPost()
        {
            errorMessage = await GetErrorMessage();
            if(errorMessage != "")
            {
                return Page();
            }
			string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registeringFan.Password);
			await _db.Database.ExecuteSqlAsync($"exec addFan {registeringFan.Name}, {registeringFan.Username} ,{hashedPassword}, {registeringFan.NationalId}, {GetSQLDateFormat(registeringFan.Birthdate)}, {registeringFan.Address}, {registeringFan.Phone}");
			return await LogUserIn();
        }
    }
}
