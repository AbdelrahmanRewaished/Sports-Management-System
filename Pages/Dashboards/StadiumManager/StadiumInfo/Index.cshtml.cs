using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Sports_Management_System.Pages.Auth;
using System.Security.Claims;

namespace Sports_Management_System.Pages.Dashboards.StadiumManager.StadiumInfo
{
    [Authorize(Roles = "StadiumManager")]
	public class IndexModel : PageModel
    {
    }
}
