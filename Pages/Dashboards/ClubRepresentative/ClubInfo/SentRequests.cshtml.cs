using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.ClubInfo
{
    public class SentRequestsModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public List<string> HostClubs { get; set; }
        public List<string> GuestClubs { get; set; }
        public List<DateTime> StartTimes { get; set; }
        public List<string> Stadiums { get; set; }
        public List<string> Statuses { get; set; }
        private Models.ClubRepresentative representative { get; set; }
        public SentRequestsModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet()
        {

            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "ClubRepresentative")
            {
                return Redirect("../../../../Auth/UnAuthorized");
            }
            representative = _db.getCurrentClubRepresentative(Username);
            HostClubs = _db.Database.SqlQuery<string>
                ($"SELECT HostClub FROM dbo.getAllHostRequestsSentBy({representative.Id})").ToList();
            GuestClubs = _db.Database.SqlQuery<string>
                ($"SELECT GuestClub FROM dbo.getAllHostRequestsSentBy({representative.Id})").ToList();
            StartTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT StartTime FROM dbo.getAllHostRequestsSentBy({representative.Id})").ToList();
            Stadiums = _db.Database.SqlQuery<string>
                ($"SELECT Stadium FROM dbo.getAllHostRequestsSentBy({representative.Id})").ToList();
            Statuses = _db.Database.SqlQuery<string>
                ($"SELECT Status FROM dbo.getAllHostRequestsSentBy({representative.Id})").ToList();
            return null;

        }
    }
}