using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JobTracker.Models;
using JobTracker.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace JobTracker.Pages.JobApplications
{
    [Authorize]
    [ValidateAntiForgeryToken] // ✅ Apply at class level for Razor Pages
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public JobApplication JobApplication { get; set; } = new(); // ✅ Prevent CS8618 warning

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            JobApplication.UserId = _userManager.GetUserId(User);
            _context.JobApplications.Add(JobApplication);
            await _context.SaveChangesAsync();

            TempData["Message"] = "✅ Job application created successfully!";
            return RedirectToPage("Index");
        }
    }
}
