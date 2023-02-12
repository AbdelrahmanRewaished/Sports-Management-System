using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.StadiumsList
{
	[Authorize(Roles = "SystemAdmin")]
	public class CreateModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string ErrorMessage = "";
        public CreateModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Stadium Stadium { get; set; }

        private async Task<string> GetErrorMessage()
        {
            if (!ModelState.IsValid)
            {
                return "Fill All Fields Correctly";
            }
            if(await _db.IsStadiumExisting(Stadium.Name!))
            {
                return "Stadium Already Exists";
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
            await _db.Database.ExecuteSqlAsync($"exec addStadium {Stadium.Name}, {Stadium.Location}, {Stadium.Capacity}");
            return RedirectToPage("Index");
        }
    }
}
