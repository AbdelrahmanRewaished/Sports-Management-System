using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.ClubsList
{
    public class CreateModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public CreateModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Club Club { get; set; }

        public async Task<IActionResult> OnGet()
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
            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();

            }
            await _db.Clubs.AddAsync(Club);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
