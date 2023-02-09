using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.ClubInfo
{
	[Authorize(Roles = "ClubRepresentative")]
	public class SentRequestsModel : PageModel
    {
    }
}