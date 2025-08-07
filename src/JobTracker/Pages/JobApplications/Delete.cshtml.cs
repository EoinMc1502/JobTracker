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
    [ValidateAntiForgeryToken]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public JobApplication JobApplication { get; set; } = new(); 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            JobApplication = await _context.JobApplications
                .Where(j => j.Id == id && j.UserId == userId)
                .FirstOrDefaultAsync();

            if (JobApplication == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(User);
            var jobApp = await _context.JobApplications
                .Where(j => j.Id == id && j.UserId == userId)
                .FirstOrDefaultAsync();

            if (jobApp == null) return NotFound();

            _context.JobApplications.Remove(jobApp);
            await _context.SaveChangesAsync();

            TempData["Message"] = "🗑️ Job application deleted successfully!";
            return RedirectToPage("Index");
        }
    }
}
