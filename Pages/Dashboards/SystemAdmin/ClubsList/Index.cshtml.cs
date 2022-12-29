using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.ClubsList
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public List<Club> Clubs { get; set; }
        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "SystemAdmin")
            {
                return Redirect("../../Auth/UnAuthorized");
            }
            Clubs = await _db.Clubs.ToListAsync();
            return null;
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var club = await _db.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            _db.Clubs.Remove(club);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
