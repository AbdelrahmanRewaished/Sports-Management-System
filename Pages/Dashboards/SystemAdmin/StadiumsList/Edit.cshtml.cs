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
        public string ErrorMessage = "";

        public async Task OnGet(int id)
        {
            Stadium = await _db.Stadia.FindAsync(id);
        }

        private async Task<string> GetErrorMessage()
        {
            if (!ModelState.IsValid)
            {
                return "Fill All Fields Correctly";
            }
            return "";
        }

        public async Task<IActionResult> OnPost()
        {
            ErrorMessage = await GetErrorMessage();
            if(ErrorMessage != "")
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
