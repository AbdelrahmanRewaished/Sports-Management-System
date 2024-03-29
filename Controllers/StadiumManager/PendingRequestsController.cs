﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;
using System.Security.Claims;

namespace Sports_Management_System.Controllers.StadiumManager
{
    [Authorize(Roles="StadiumManager")]
    [Route("api/pending-requests")]
    [ApiController]
    public class PendingRequestsController : Controller
    {
        private readonly ChampionsLeagueDbContext _db;
        public PendingRequestsController(ChampionsLeagueDbContext db)
        {
            _db = db;  
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingRequests()
        {
            string Username = Auth.GetCurrentUserName(User);

			return Json(new {data = await _db.GetPendingRequests(Username).ToListAsync()}); 
        }

        [HttpPost]
        public async Task<IActionResult> HandleRequest(string HostClub, string GuestClub, DateTime startTime, bool accepting)
        {
            string Username = Auth.GetCurrentUserName(User);
			string time = getRequiredSQLDateFormat(startTime);
            if (accepting)
            {
                await _db.Database.ExecuteSqlAsync($"exec acceptRequest {Username},{HostClub},{GuestClub},{time}");
            }
            else
            {
                await _db.Database.ExecuteSqlAsync($"exec rejectRequest {Username},{HostClub},{GuestClub},{time}");
            }
            return Json(new { success = true, message = accepting ? "Request Accepted Successfully" : "Request Rejected Successfully" });
        }
        private static string getRequiredSQLDateFormat(DateTime dateTime)
        {
            string req_format = dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day + " " +
                dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second;

            return req_format;
        }
    }
}
