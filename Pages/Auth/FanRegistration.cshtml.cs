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

            public int NationalId { get; set; }

            public string Phone { get; set; }

            public DateTime Birthdate { get; set; }

            public string Address { get; set; }
        }

        [BindProperty]
        public FanRegWrapper registeringFan { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid || !registeringFan.Password.Equals(registeringFan.ConfirmPassword))
            {
                errorMessage = "Passwords must match";
                return Page();
            }
            SystemUser user = await _db.SystemUsers.FindAsync(registeringFan.Username);
            if (user != null)
            {
                errorMessage = "Already registered";
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addFan {registeringFan.Name}, {registeringFan.Username} ,{registeringFan.Password}, {registeringFan.NationalId}, {registeringFan.Birthdate}, {registeringFan.Address}, {registeringFan.Phone}");
            return RedirectToPage("Login");
        }
    }
}
