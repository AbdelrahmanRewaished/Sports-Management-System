using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
	[Authorize(Roles = "AssociationManager")]
	public class AlreadyPlayedMatchesModel : PageModel
    {
    }
}
