using System;
using System.ComponentModel.DataAnnotations;

namespace JobTracker.Models
{
    public enum ApplicationStatus
    {
        Applied,
        Interviewing,
        Offer,
        Rejected
    }

    public class JobApplication
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public ApplicationStatus Status { get; set; }
        public DateTime DateApplied { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }

}
