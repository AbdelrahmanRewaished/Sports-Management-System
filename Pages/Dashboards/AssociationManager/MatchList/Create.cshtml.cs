using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class CreateModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public CreateModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public AllUpComingMatch Match {get; set; }


        public async Task<IActionResult> OnGet()
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
            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }
            await _db.Database.ExecuteSqlAsync($"exec addNewMatch {Match.HostClub}, {Match.GuestClub}, {Match.StartTime}, {Match.EndTime}");
            return RedirectToPage("Index");
        }
    }
}
