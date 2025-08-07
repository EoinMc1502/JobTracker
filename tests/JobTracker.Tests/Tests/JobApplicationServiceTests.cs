using Xunit;
using JobTracker.Data;
using JobTracker.Models;
using JobTracker.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace JobTracker.Tests
{
    public class JobApplicationServiceTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // unique DB per test
                .Options;

            return new ApplicationDbContext(options);
        }

        private async Task SeedDataAsync(ApplicationDbContext context)
        {
            var apps = new List<JobApplication>
            {
                new JobApplication
                {
                    CompanyName = "Google",
                    Position = "SWE",
                    Status = ApplicationStatus.Applied,
                    UserId = "user1",
                    DateApplied = DateTime.UtcNow,
                    Notes = "Excited about this opportunity"
                },
                new JobApplication
                {
                    CompanyName = "Amazon",
                    Position = "DevOps",
                    Status = ApplicationStatus.Offer,
                    UserId = "user1",
                    DateApplied = DateTime.UtcNow,
                    Notes = "Offer received, pending decision"
                },
                new JobApplication
                {
                    CompanyName = "Meta",
                    Position = "Frontend",
                    Status = ApplicationStatus.Rejected,
                    UserId = "user2",
                    DateApplied = DateTime.UtcNow,
                    Notes = "Not a great interview experience"
                },
            };

            context.JobApplications.AddRange(apps);
            await context.SaveChangesAsync();
        }

        [Fact]
        public async Task GetApplicationsByUserIdAsync_ReturnsCorrectUserApps()
        {
            var context = GetInMemoryDbContext();
            await SeedDataAsync(context);

            var service = new JobApplicationService(context);
            var result = await service.GetApplicationsByUserIdAsync("user1");

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetStatusCountsAsync_ReturnsCorrectStatusCount()
        {
            var context = GetInMemoryDbContext();
            await SeedDataAsync(context);

            var service = new JobApplicationService(context);
            var counts = await service.GetStatusCountsAsync("user1");

            Assert.Equal(1, counts[ApplicationStatus.Applied]);
            Assert.Equal(1, counts[ApplicationStatus.Offer]);
        }
    }
}
