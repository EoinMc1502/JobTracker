using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JobTracker.Models;
using JobTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


namespace JobTracker.Pages.JobApplications
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public JobApplication JobApplication { get; set; } = new(); 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "❌ Invalid request: ID not provided.";
                return RedirectToPage("Index");
            }

            var userId = _userManager.GetUserId(User);
            JobApplication = await _context.JobApplications
                .Where(j => j.Id == id && j.UserId == userId)
                .FirstOrDefaultAsync()!;

            if (JobApplication == null)
            {
                TempData["Error"] = "❌ Job application not found or you do not have permission to view it.";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
