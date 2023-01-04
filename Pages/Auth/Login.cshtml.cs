using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;

namespace Sports_Management_System.Pages.Auth
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
            string username = HttpContext.Session.GetString("Username");
            string role = HttpContext.Session.GetString("Role");
            if(username != null)
            {
                HttpContext.Session.Remove("Username");
                HttpContext.Session.Remove("Role");
            }

        }

        public async Task<IActionResult> OnPost()
        {
            username = Request.Form["username"]!;
            password = Request.Form["password"]!;
            SystemUser user = await _db.SystemUsers.FindAsync(username);

            if (user == null) { 
                usernameError = "User Does not Exist";
                return Page();
            }
            if(! user.Password!.Equals(password))
            {
                passwordError = "Passwords is Incorrect";
                return Page();
            }

            string dashboardFolder = "~/Dashboards/";
            
            // check if user is a Fan 
            var fanList = _db.Fans
                 .Where(n => n.Username == username)
                 .ToList();
            if (fanList.Count > 0)
            {
                Fan fan = fanList.ElementAt(0);
                if (!(bool)fan.Status!)
                {
                    return Redirect("Blocked");
                }
                return LogUserIn(dashboardFolder, "Fan", username!, "Fan");
            }
           
            // check if user is a stadium manager 
            var stadiumManagerList = _db.StadiumManagers
                 .Where(n => n.Username == username)
                 .ToList();
            if (stadiumManagerList.Count > 0)
            {
                return LogUserIn(dashboardFolder, "StadiumManager", username!, "StadiumManager");

            }

            // check if user is a association manager 
            var assocManagerList = _db.SportsAssociationManagers
                 .Where(n => n.Username == username)
                 .ToList();
            if (assocManagerList.Count > 0)
            {
                return LogUserIn(dashboardFolder, "AssociationManager", username!, "AssociationManager");

            }

            // check if user is a club representative 
            var clubRepresentativeList = _db.ClubRepresentatives
                 .Where(n => n.Username == username)
                 .ToList();
            if (clubRepresentativeList.Count > 0)
            {
                return LogUserIn(dashboardFolder, "ClubRepresentative", username!, "ClubRepresentative");

            }
            return LogUserIn(dashboardFolder, "SystemAdmin", username!, "SystemAdmin");
        }

        private IActionResult LogUserIn(string destinationFolder,string destinationFile, string username, string role)
        {
            string destination = destinationFolder + destinationFile;
            HttpContext.Session.SetString("Username", username);
            HttpContext.Session.SetString("Role", role);
            return Redirect(destination);
        }

    }
}
