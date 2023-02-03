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
        private IActionResult LogUserIn(string username, string role)
        {
            Login.Auth.setUserSession(HttpContext, username, role);
            string destination = Login.Auth.getLoggedUserDestination(role);
            return Redirect(destination);
        }

        public async Task<IActionResult> OnPost()
        {
            if(! ModelState.IsValid)
            {
                errorMessage = "Fill All Fields Correctly";
                return Page();
            }
			if (registeringAssocManager.Password.Length < 6)
			{
				errorMessage = "Password must be longer than 5 characters";
				return Page();
			}
			if (registeringAssocManager.Password.Length > 20)
			{
				errorMessage = "Password must be shorter than 20 characters";
				return Page();
			}
			if (!registeringAssocManager.Password.Equals(registeringAssocManager.ConfirmPassword))
			{
				errorMessage = "Passwords must match";
				return Page();
			}
			SystemUser user = await _db.SystemUsers.FindAsync(registeringAssocManager.Username);
            if(user != null)
            {
                errorMessage = "Already registered";
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addAssociationManager {registeringAssocManager.Name}, {registeringAssocManager.Username}, {registeringAssocManager.Password}");
            return LogUserIn(registeringAssocManager.Username, "AssociationManager");
        }
    }
}
