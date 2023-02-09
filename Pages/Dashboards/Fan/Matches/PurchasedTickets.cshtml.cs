using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan.Matches
{
	[Authorize(Roles = "Fan")]
	public class PurchasedTicketsModel : PageModel
    {
    }
}
