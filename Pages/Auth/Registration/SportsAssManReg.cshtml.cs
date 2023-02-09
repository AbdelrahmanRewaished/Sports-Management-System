using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sports_Management_System.Pages.Auth
{
    public class SportsAssManRegModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string errorMessage = "";
        public SportsAssManRegModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        
        public class AssocManagerRegWrapper
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            [DataType(DataType.Password), Compare(nameof(Password))]
            public string ConfirmPassword { get; set; }
        }
        [BindProperty]
        public AssocManagerRegWrapper registeringAssocManager { get; set; }
        private IActionResult LogUserIn(string username)
        {
            Auth.SetUserClaims(HttpContext, username, Auth.AssociationManagerRole);
            string destination = Auth.GetLoggingUserDestination(Auth.AssociationManagerRole);
            return Redirect(destination);
        }

        private async Task<string> GetErrorMessage()
        {
			if (!ModelState.IsValid)
			{
				return "Fill All Fields Correctly";
			}
			string errorMessage = Auth.GetPasswordErrorMessage(registeringAssocManager.Password, registeringAssocManager.ConfirmPassword);
			if (errorMessage != "")
			{
                return errorMessage;
			}
			if (await Auth.IsUserExisting(_db, registeringAssocManager.Username))
			{
				return "Already registered";
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
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registeringAssocManager.Password);
			await _db.Database.ExecuteSqlAsync($"exec addAssociationManager {registeringAssocManager.Name}, {registeringAssocManager.Username}, {hashedPassword}");
			return LogUserIn(registeringAssocManager.Username);
        }
    }
}
