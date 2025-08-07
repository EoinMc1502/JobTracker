using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JobTracker.Data;
using JobTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace JobTracker.Pages.JobApplications
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<JobApplication> JobApplicationList { get; set; } = new List<JobApplication>();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public ApplicationStatus? StatusFilter { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            var query = _context.JobApplications
                .Where(j => j.UserId == userId);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(j =>
                    j.CompanyName.Contains(SearchTerm) ||
                    j.Position.Contains(SearchTerm));
            }

            if (StatusFilter.HasValue)
            {
                query = query.Where(j => j.Status == StatusFilter.Value);
            }

            JobApplicationList = await query
                .OrderByDescending(j => j.DateApplied)
                .ToListAsync();

            return Page();
        }
    }
}
