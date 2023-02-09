using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;
using System.Data;
using System.Security.Claims;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.StadiumsList
{
    [Authorize(Roles = "ClubRepresentative")]
	public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public List<AvailableStadium> AvailableStadiums;
        public string clubName { get; set; }
        public string HostClub {get; set;}
        public string GuestClub { get; set; }
        public DateTime StartTime { get; set; }
        public string Username { get; set; }

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public async Task OnGet(string HostClub, string GuestClub, DateTime StartTime)
        {
            Username = Auth.Auth.GetCurrentUserName(User);
            this.HostClub = HostClub;
            this.GuestClub = GuestClub;
            this.StartTime = StartTime;
            AvailableStadiums = await _db.ViewAvailableStadiumsOn(HostClub, GuestClub, StartTime).ToListAsync();
            HttpContext.Session.SetString("MatchStartTime", StartTime.ToString());
        }
       
        public async Task<IActionResult> OnPost(string Stadium)
        {
            Username = Auth.Auth.GetCurrentUserName(User);
			Club club = await _db.Clubs.FindAsync((await _db.GetCurrentClubRepresentative(Username)).ClubId);
            clubName = club!.Name!;
            string MatchStartTime = HttpContext.Session.GetString("MatchStartTime")!;
            await _db.Database.ExecuteSqlAsync($"exec AddHostRequest {clubName}, {Stadium}, {MatchStartTime}");

            HttpContext.Session.Remove("MatchStartTime");
            return Redirect("ClubInfo");
        }

        public async Task<bool> isRequestRejectedAsync(string Stadium)
        {
            return (bool)await _db.IsRequestRejected(Username, HostClub, GuestClub, StartTime, Stadium);
        }
    }
}
