using BugTrackingSystem.Data;
using BugTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create Story
        [HttpPost]
        public async Task<IActionResult> CreateStory([FromBody] Story story)
        {
            if (story == null || string.IsNullOrEmpty(story.Name) || string.IsNullOrEmpty(story.Description) || story.ProjectId == 0)
            {
                return BadRequest("All fields are required.");
            }

            try
            {
                _context.Stories.Add(story);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStoryById), new { id = story.Id }, story); // return the created story
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get all stories for a project
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetStoriesByProject(int projectId)
        {
            var stories = await _context.Stories
                .Where(s => s.ProjectId == projectId)
                .ToListAsync();

            return Ok(stories);
        }

        // Get story by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoryById(int id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }
            return Ok(story);
        }

        // Delete story
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }

            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
