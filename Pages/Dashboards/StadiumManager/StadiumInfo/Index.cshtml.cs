using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager.StadiumInfo
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public Stadium Stadium;
        public List<string> RepresentativeNames;
        public List<string> HostNames;
        public List<string> GuestNames;
        public List<DateTime> StartTimes;
        public List<DateTime> EndTimes;
        public List<string> Statuses;

        public int MatchId;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
            Stadium = _db.Stadia.Find(StadiumManager.IndexModel.StadiumManager.StadiumId);
            RepresentativeNames = _db.Database.SqlQuery<string>
                ($"SELECT club_rep_name FROM dbo.allPendingRequests({StadiumManager.IndexModel.StadiumManager.Username})")
                .ToList();

            HostNames = _db.Database.SqlQuery<string>
                ($"SELECT HostClub FROM dbo.allPendingRequests({StadiumManager.IndexModel.StadiumManager.Username})")
                .ToList();

            GuestNames = _db.Database.SqlQuery<string>
                ($"SELECT GuestClub FROM dbo.allPendingRequests({StadiumManager.IndexModel.StadiumManager.Username})")
                .ToList();


            StartTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT StartTime FROM dbo.allPendingRequests({StadiumManager.IndexModel.StadiumManager.Username})")
                .ToList();

            EndTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT EndTime FROM dbo.allPendingRequests({StadiumManager.IndexModel.StadiumManager.Username})")
                .ToList();

            Statuses = _db.Database.SqlQuery<string>
                ($"SELECT Status FROM dbo.allPendingRequests({StadiumManager.IndexModel.StadiumManager.Username})")
                .ToList();
        }
        public void OnGet()
        {
            
        }

        private static string getRequiredSQLDateFormat(DateTime dateTime)
        {
            string req_format = dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day + " " +
                dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second;

            return req_format;
        }

        public async Task<IActionResult> OnPostAccept(string HostClub, string GuestClub, DateTime startTime)
        {
            string time = getRequiredSQLDateFormat(startTime);
            _db.Database.ExecuteSql($"exec acceptRequest {StadiumManager.IndexModel.StadiumManager.Username},{HostClub},{GuestClub},{time}");
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostReject(string HostClub, string GuestClub, DateTime startTime)
        {
            string time = getRequiredSQLDateFormat(startTime);
            _db.Database.ExecuteSql($"exec rejectRequest {StadiumManager.IndexModel.StadiumManager.Username},{HostClub},{GuestClub},{time}");
            return RedirectToPage("Index");
        }
    }
}
