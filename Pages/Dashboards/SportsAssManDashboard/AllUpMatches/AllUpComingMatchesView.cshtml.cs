using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Sports_Management_System.Models;
using System.Data;

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
            DataTable data = new DataTable();
            data = await  _db.Database.ExecuteSqlAsync($"exec dbo.upcomingMatchesOfClub()"));
            
            Matches = (from data  in data.Rows
                      select new Match()
                      {
                          HostClub = data["HostClub"].ToString(),
                          GuestClub = data["GuestCLub"].ToString(),
                          StartTime = data["StartTime"].ToString(),
                          EndTime = data["EndTime"].ToString()
                      }).ToList();

        }
    }
}
