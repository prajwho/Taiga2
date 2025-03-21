namespace BugTrackingSystem.Models
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string ApplicationUserId { get; set; }//fk to application user
        public List<ProjectMember> Members { get; set; } = new List<ProjectMember>();//who can access this
        public List<Sprint> Sprints { get; set; } = new List<Sprint>();//all the sprints created in this 
        public List<Story> Stories { get; set; }= new List<Story>();//all the stories or project backlog for this project

    }
}
