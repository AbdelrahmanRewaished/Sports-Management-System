using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.authentication
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
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (! ModelState.IsValid || ! registeringStadiumManager.Password.Equals(registeringStadiumManager.ConfirmPassword))
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
                .FromSql($"SELECT * FROM Stadium")
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
            
            if(stadiumManagers.Count != 0)
            {
                errorMessage = "A Stadium Manager already exists ";
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addStadiumManager {registeringStadiumManager.Name}, {registeringStadiumManager.Entity} ,{registeringStadiumManager.Username}, {registeringStadiumManager.Password}");
            return RedirectToPage("Login");
        }
    }
    
}
