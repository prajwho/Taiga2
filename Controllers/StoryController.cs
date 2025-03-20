using BugTrackingSystem.Data;
using BugTrackingSystem.Dto;
using BugTrackingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Controllers
{
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new story
        [HttpPost("api/story")]
        public async Task<IActionResult> CreateStory([FromBody] CreateStoryDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Description) )
            {
                return BadRequest("All fields are required.");
            }

            var story = new Story
            {
                Name = dto.Name,
                Description = dto.Description,
                ProjectId = dto.ProjectId
            };

            try
            {
                _context.Stories.Add(story);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStoryById), new { id = story.Id }, story);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get story by id
        [HttpGet("api/story/{id}")]
        public async Task<IActionResult> GetStoryById(int id)
        {
            var story = await _context.Stories
                .FirstOrDefaultAsync(s => s.Id == id);

            if (story == null)
            {
                return NotFound();
            }

            return Ok(story);
        }

        // Get all backlog stories for a project
        [HttpGet("api/stories/{projectId}")]
        public async Task<IActionResult> GetBacklog(int projectId)
        {
            var stories = await _context.Stories
                .ToListAsync();

            return Ok(stories);
        }
        [HttpGet("api/stories")]
        public async Task<IActionResult> GetAllStories()
        {
            var stories = await _context.Stories
                .ToListAsync();

            return Ok(stories);
        }
    }
}
