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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public JobApplication JobApplication { get; set; }

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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var userId = _userManager.GetUserId(User);
            var existingApp = await _context.JobApplications
                .Where(j => j.Id == JobApplication.Id && j.UserId == userId)
                .FirstOrDefaultAsync();

            if (existingApp == null) return NotFound();

            existingApp.CompanyName = JobApplication.CompanyName;
            existingApp.Position = JobApplication.Position;
            existingApp.DateApplied = JobApplication.DateApplied;
            existingApp.Status = JobApplication.Status;
            existingApp.Notes = JobApplication.Notes;

            await _context.SaveChangesAsync();

            TempData["Message"] = "✏️ Job application updated successfully!";
            return RedirectToPage("Index");
        }
    }
}
