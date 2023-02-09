using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Sports_Management_System.Models;
using System.Data;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
	[Authorize(Roles = "AssociationManager")]
	public class AllUpComingMatchesViewModel : PageModel
    {
    }
}
