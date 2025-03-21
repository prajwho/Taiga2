namespace BugTrackingSystem.Dto
{
    public class CreateStoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int ProjectId { get; set; }

    }
}
