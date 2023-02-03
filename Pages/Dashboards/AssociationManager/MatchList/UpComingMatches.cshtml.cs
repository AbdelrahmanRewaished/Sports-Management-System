using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Sports_Management_System.Models;
using System.Data;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class AllUpComingMatchesViewModel : PageModel
    {
        public IActionResult? OnGet()
        {
            string path = AssociationManager.IndexModel.getRedirectionPath(HttpContext);
            if (path != null)
            {
                return Redirect(path);
            }
            return null;
        }
    }
}
