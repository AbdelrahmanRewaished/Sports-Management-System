using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.StadiumsList
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public List<Stadium> Stadiums { get; set; }
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
            Stadiums = await _db.Stadia.ToListAsync();
            return null;
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Stadium = await _db.Stadia.FindAsync(id);
            if (Stadium == null)
            {
                return NotFound();
            }
            _db.Stadia.Remove(Stadium);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
