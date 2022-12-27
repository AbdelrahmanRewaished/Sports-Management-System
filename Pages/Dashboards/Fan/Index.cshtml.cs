using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
    
        public string Username { get; set; }
        public static Models.Fan fan { get; set; }
        public void OnGet(string username)
        {
            Username = username;
            fan = _db.Fans
                    .Where(n => n.Username == username)
                    .OrderBy(n => n.Username)
                    .Last();
        }
    }
}
