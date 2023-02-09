using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sports_Management_System.Pages.Dashboards.SystemAdmin.StadiumsList
{
	[Authorize(Roles = "SystemAdmin")]
	public class IndexModel : PageModel
    {
    }
}
