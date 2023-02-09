using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.StadiumsList
{
	[Authorize(Roles = "SystemAdmin")]
	public class EditModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public EditModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Stadium Stadium { get; set; }
        public async Task OnGet(int id)
        {
            Stadium = await _db.Stadia.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var StadiumFromDb = await _db.Stadia.FindAsync(Stadium.Id);
            StadiumFromDb.Name = Stadium.Name;
            StadiumFromDb.Location = Stadium.Location;
            StadiumFromDb.Capacity = Stadium.Capacity;
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
