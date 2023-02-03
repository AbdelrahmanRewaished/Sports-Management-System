using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Auth
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string username, password;
        public string usernameError;
        public string passwordError;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            string username = HttpContext.Session.GetString("Username")!;
            string role = HttpContext.Session.GetString("Role")!;
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

            return LogUserIn(username, getUserRole(username));
           
        }

        private string? getUserRole(string Username)
        {
            if(_db.isFan(Username))
            {
                if(!(bool)_db.getCurrentFan(Username).Status!)
                {
                    return "Blocked";
                }
                return "Fan";
            }
            if(_db.isClubRepresentative(Username))
            {
                return "ClubRepresentative";
            }
            if(_db.isAssociationManager(Username))
            {
                return "AssociationManager";
            }
            if(_db.isStadiumManager(Username))
            {
                return "StadiumManager";
            }
            return "SystemAdmin";
        }

        private IActionResult? LogUserIn(string username, string role)
        {
            if(role == "Blocked")
            {
                return RedirectToPage("Blocked");
            }
            Login.Auth.setUserSession(HttpContext, username, role);
            string destination = Login.Auth.getLoggedUserDestination(role);
            return Redirect(destination);
        }

    }
}
