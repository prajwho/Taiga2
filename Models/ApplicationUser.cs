using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BugTrackingSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public required string FullName { get; set; }
        public List<Project> CreatedProjects { get; set; } = new List<Project>();
     
    }
}
