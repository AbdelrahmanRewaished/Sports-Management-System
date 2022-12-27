using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public static Models.StadiumManager StadiumManager { get; set; }
        public string Username { get; set; }
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        
        public void OnGet(string username)
        {
            Username = username;
            StadiumManager = _db.StadiumManagers
                                    .FromSql($"SELECT * FROM Stadium_Manager")
                                    .Where(n => n.Username == username)
                                    .OrderBy(n => n.Username)
                                    .Last();
        }
    }
}
