using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public string Username { get; set; }
        public static Models.ClubRepresentative clubRepresentative { get; set; }

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
    
        public void OnGet(string username)
        {
            Username = username;
            clubRepresentative = _db.ClubRepresentatives
                                    .FromSql($"SELECT * FROM Club_Representative")
                                    .Where(n => n.Username == username)
                                    .OrderBy(n => n.Username)
                                    .Last();
        }
    }
}
