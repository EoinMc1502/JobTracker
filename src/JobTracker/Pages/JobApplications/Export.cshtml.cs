using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JobTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


namespace JobTracker.Pages.JobApplications
{
    [Authorize]
    public class ExportModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExportModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var applications = await _context.JobApplications
                .Where(j => j.UserId == userId)
                .OrderByDescending(j => j.DateApplied)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("Company,Position,DateApplied,Status,Notes");

            foreach (var app in applications)
            {
                csv.AppendLine($"\"{app.CompanyName}\",\"{app.Position}\",\"{app.DateApplied:yyyy-MM-dd}\",\"{app.Status}\",\"{app.Notes?.Replace("\"", "\"\"")}\"");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "JobApplications.csv");
        }
    }
}
