using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Sports_Management_System.Pages.authentication
{
    public class SportsAssManRegModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public SportsAssManRegModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        
        public class AssocManagerRegWrapper
        {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            [DataType(DataType.Password), Compare(nameof(Password))]
            public string ConfirmPassword { get; set; }
        }
        [BindProperty]
        public AssocManagerRegWrapper registeringAssocManager { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(! ModelState.IsValid)
            {
                return Page();
            }
            SystemUser user = await _db.SystemUsers.FindAsync(registeringAssocManager.Username);
            if(user != null)
            {
                return Page();
            }
            if(!registeringAssocManager.Password.Equals(registeringAssocManager.ConfirmPassword))
            {
                return Page();
            }
            await _db.Database.ExecuteSqlAsync($"exec addAssociationManager {registeringAssocManager.Name}, {registeringAssocManager.Username}, {registeringAssocManager.Password}");
            return RedirectToPage("Login");
        }
    }
}
