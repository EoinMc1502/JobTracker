using JobTracker.Data;
using JobTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Services
{
    public class JobApplicationService
    {
        private readonly ApplicationDbContext _context;

        public JobApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobApplication>> GetApplicationsByUserIdAsync(string userId)
        {
            return await _context.JobApplications
                .Where(j => j.UserId == userId)
                .ToListAsync();
        }

        public async Task<Dictionary<ApplicationStatus, int>> GetStatusCountsAsync(string userId)
        {
            var applications = await _context.JobApplications
                .Where(j => j.UserId == userId)
                .ToListAsync();

            return applications
                .GroupBy(j => j.Status)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
