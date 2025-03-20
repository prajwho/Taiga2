namespace BugTrackingSystem.Models
{
    public class ProjectMember
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public required string UserId { get; set; }
        public RoleType Role { get; set; } //  Product Owner,Project Manager, Developer, QA, Viewer
    }
}
