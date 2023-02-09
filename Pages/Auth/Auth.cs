using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Sports_Management_System.Models;
using System.Security.Claims;

namespace Sports_Management_System.Pages.Auth
{
    public class Auth: ControllerBase
    {
        private static readonly string DESTINATION_FOLDER = "~/Dashboards/";
        private static readonly string COOKIE_AUTH = "CookieAuth";

        public static readonly string RepresentativeRole = "ClubRepresentative";
        public static readonly string AdminRole = "SystemAdmin";
        public static readonly string FanRole = "Fan";
        public static readonly string AssociationManagerRole = "AssociationManager";
        public static readonly string StadiumManagerRole = "StadiumManager";

        public static async Task SetUserClaims(HttpContext HttpContext, string username, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, COOKIE_AUTH);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(COOKIE_AUTH, claimsPrincipal, new AuthenticationProperties { IsPersistent = false });
        }

        public static void SignUserOut(HttpContext httpContext)
        {
            httpContext.SignOutAsync(COOKIE_AUTH);
        }
        public static string GetLoggingUserDestination(string role)
        {
            return DESTINATION_FOLDER + role;
        }

        public static string GetCurrentUserName(ClaimsPrincipal User)
        {
            return User.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;
        }

        public static async Task<string> GetUserRole(string Username, ChampionsLeagueDbContext _db)
        {
            if (await _db.IsAdmin(Username))
            {
                return AdminRole;
            }
            if (await _db.IsAssociationManager(Username))
            {
                return AssociationManagerRole;
            }
            if (await _db.IsStadiumManager(Username))
            {
                return StadiumManagerRole;
            }
            if (await _db.IsClubRepresentative(Username))
            {
                return RepresentativeRole;
            }
            return FanRole;
        }

        public static async Task<bool> IsUserExisting(ChampionsLeagueDbContext _db, string username)
        {
            SystemUser User = await _db.SystemUsers.FindAsync(username);
            return User != null;
        }
        public static string GetPasswordErrorMessage(string Password, string ConfirmedPassword)
        {
            if (Password.Length < 7)
            {
                return "Password must be longer than 6 characters";
            }
            if (Password.Length > 20)
            {
                return "Password must be shorter than 20 characters";
            }
            if (!Password.Equals(ConfirmedPassword))
            {
                return "Passwords must match";
            }
            return "";
        }
    }
}
