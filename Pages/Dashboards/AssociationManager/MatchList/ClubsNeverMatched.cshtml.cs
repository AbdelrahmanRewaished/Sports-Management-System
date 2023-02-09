using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
	[Authorize(Roles = "AssociationManager")]
	public class ClubsNeverMatchedModel : PageModel
    {
    }
}
