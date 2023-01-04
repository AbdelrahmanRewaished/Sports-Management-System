using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public List<Match> Matches { get; set; }
        public List<AllMatch> AllMatches { get; set; }
       
        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "AssociationManager")
            {
                return Redirect("../../../Auth/UnAuthorized");
            }
            AllMatches = await _db.AllMatches.ToListAsync();
            Matches = await _db.Matches.ToListAsync();
            return null;
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Match = await _db.Matches.FindAsync(id);
            if (Match == null)
            {
                return NotFound();
            }
            _db.Matches.Remove(Match);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}