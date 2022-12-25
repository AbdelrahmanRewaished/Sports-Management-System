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


        public void OnGet()
        {
        }
    }
}
