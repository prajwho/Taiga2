namespace BugTrackingSystem.Dto
{
    public class CreateSprintDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectId { get; set; }
    }
}
