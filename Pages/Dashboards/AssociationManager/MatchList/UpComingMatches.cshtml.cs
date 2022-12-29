using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Sports_Management_System.Models;
using System.Data;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class AllUpComingMatchesViewModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public List<AllUpComingMatch> Matches { get; set; }
        public AllUpComingMatchesViewModel(ChampionsLeagueDbContext db)
        {
            _db = db;
            Matches = _db.AllUpComingMatches.ToList();
        }


        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "AssociationManager")
            {
                return Redirect("../../../../Auth/UnAuthorized");
            }
            return null;
        }
    }
}
