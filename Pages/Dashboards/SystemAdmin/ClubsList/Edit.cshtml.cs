using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.ClubsList
{
    public class EditModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public EditModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Club Club { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "SystemAdmin")
            {
                return Redirect("../../Auth/UnAuthorized");
            }
            Club = await _db.Clubs.FindAsync(id);
            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var ClubFromDb = await _db.Clubs.FindAsync(Club.ClubId);
            ClubFromDb.Name = Club.Name;
            ClubFromDb.Location = Club.Location;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}