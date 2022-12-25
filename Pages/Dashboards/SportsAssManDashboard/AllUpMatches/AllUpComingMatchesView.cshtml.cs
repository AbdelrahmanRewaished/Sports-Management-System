using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.SportsAssManDashboard.AllUpMatches
{
    public class AllUpComingMatchesViewModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public AllUpComingMatchesViewModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public List<Match> Matches { get; set; }
        
        public  async void OnGet()
        {

        }
    }
}
