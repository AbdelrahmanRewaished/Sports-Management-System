using Microsoft.AspNetCore.Mvc;

namespace Sports_Management_System.Pages.Auth.Login
{
    public static class Auth
    {
        private static string DESTINATION_FOLDER = "~/Dashboards/";
        public static string getRedirectionPath(HttpContext httpContext, string role)
        {
            if (!isAuthenticated(httpContext))
            {
                return "~/Auth/Login";
            }
            if (!isAuthorized(httpContext, role))
            {
                return "~/Auth/Login/UnAuthorized";
            }
            return null;
        }
        private static bool isAuthenticated(HttpContext httpContext)
        {
            return httpContext.Session.GetString("Username") != null;
        }

        private static bool isAuthorized(HttpContext httpContext, string role)
        {
            string Role = httpContext.Session.GetString("Role")!;
            return Role == role;
        }
        public static void setUserSession(HttpContext HttpContext, string username, string role)
        {
            HttpContext.Session.SetString("Username", username);
            HttpContext.Session.SetString("Role", role);
        }
        public static string getLoggedUserDestination(string role)
        {
            return DESTINATION_FOLDER + role;
        }
    }
}
