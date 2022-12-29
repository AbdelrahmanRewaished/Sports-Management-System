using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class EditModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public EditModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Match Match { get; set; }
        public string HostClub { get; set; }
        public string GuestClub { get; set; }

        public async Task<IActionResult> OnGet( int Id, string hostClub, string guestClub)
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "AssociationManager")
            {
                return Redirect("../../../../Auth/UnAuthorized");
            }  
            Match = await  _db.Matches.FindAsync(Id);
            HostClub = hostClub;
            GuestClub = guestClub;
            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            string hostClub = Request.Form["hostClub"];
            string guestClub = Request.Form["guestClub"];
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec updateMatch {Match.MatchId}, {hostClub}, {guestClub}, {Match.StartTime}, {Match.EndTime}");
            return RedirectToPage("Index");
        }

    }
}
