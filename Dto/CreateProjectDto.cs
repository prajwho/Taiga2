using System.ComponentModel.DataAnnotations;

namespace BugTrackingSystem.Dto
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Project name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }
    }
}
