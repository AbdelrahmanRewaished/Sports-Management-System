using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.authentication
{
    public class LoginModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string username, password;
        public string usernameError;
        public string passwordError;

        public LoginModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            username = Request.Form["username"];
            password = Request.Form["password"];
            SystemUser user = await _db.SystemUsers.FindAsync(username);

            if (user == null) { 
                usernameError = "User Does not Exist";
                return Page();
            }
            if(! user.Password.Equals(password))
            {
                passwordError = "User Does not Exist";
                return Page();
            }

            string dashboardFolder = "~/Dashboards/";

            // check if user is a Fan 
            var fanList = _db.Fans.FromSql($"SELECT * FROM Fan")
                 .Where(n => n.Username == username)
                 .ToList();
            if (fanList.Count > 0)
            {
                return RedirectToPage(dashboardFolder + "FanDashboard");
            }
            // check if user is a stadium manager 
            var stadiumManagerList = _db.StadiumManagers
                 .FromSql($"SELECT * FROM Stadium_Manager")
                 .Where(n => n.Username == username)
                 .ToList();
            if (stadiumManagerList.Count > 0)
            {
                return RedirectToPage(dashboardFolder + "StadiumManagerDashboard");
            }
            // check if user is a association manager 
            var assocManagerList = _db.SportsAssociationManagers
                 .FromSql($"SELECT * FROM Sports_association_manager")
                 .Where(n => n.Username == username)
                 .ToList();
            if (assocManagerList.Count > 0)
            {
                return RedirectToPage(dashboardFolder + "AssociationManagerDashboard");
            }
            // check if user is a club representative 
            var clubRepresentativeList = _db.ClubRepresentatives
                 .FromSql($"SELECT * FROM Club_Representative")
                 .Where(n => n.Username == username)
                 .ToList();
            if (clubRepresentativeList.Count > 0)
            {
                return Redirect(dashboardFolder + "ClubRepresentativeDashboard");
            }

            return Page();
        }

    }
}