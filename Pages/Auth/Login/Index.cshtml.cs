using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Auth
{
	[IgnoreAntiforgeryToken]
	public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public bool error;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
       
        public async Task OnGet()
        {
            if(User.Identity!.IsAuthenticated)
            {
                await Auth.SignUserOut(HttpContext);
            }
        }

		public async Task<IActionResult> OnPost()
        {
            string Username = Request.Form["Username"]!;
            string Password = Request.Form["Password"]!;
            SystemUser user = await _db.SystemUsers.FindAsync(Username);

            if (user == null || ! BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                error = true;
                return Page();
            }
            string role = await Auth.GetUserRole(Username, _db);
            if(role == Auth.FanRole)
            {
                Fan Fan = await _db.GetCurrentFan(Username);
                bool FanBlocked = !(bool)Fan.Status!;
                if(FanBlocked)
                {
                    return RedirectToPage("Blocked");
                }
            }
            await Auth.SetUserClaims(HttpContext, Username, role);
            string destination = Auth.GetLoggingUserDestination(role);
            return Redirect(destination);
        }

        
    }
}
