using BugTrackingSystem.Data;
using BugTrackingSystem.Dto;
using BugTrackingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Controllers
{
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SprintController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new sprint
        [HttpPost("api/sprint")]
        public async Task<IActionResult> CreateSprint([FromBody] CreateSprintDto dto)
        {
            if (dto == null || dto.StartDate == DateTime.MinValue || dto.EndDate == DateTime.MinValue || dto.ProjectId <= 0)
            {
                return BadRequest("All fields are required.");
            }

            var sprint = new Sprint
            {
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ProjectId = dto.ProjectId
            };

            try
            {
                _context.Sprints.Add(sprint);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSprintById), new { id = sprint.Id }, sprint);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get sprint by id
        [HttpGet("api/sprint/{id}")]
        public async Task<IActionResult> GetSprintById(int id)
        {
            var sprint = await _context.Sprints
                .Include(s => s.Stories)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sprint == null)
            {
                return NotFound();
            }

            return Ok(sprint);
        }

        // Get sprints by project id
        [HttpGet("api/sprintby/{projectId}")]
        public async Task<IActionResult> GetSprintsByProject(int projectId)
        {
            var sprints = await _context.Sprints
                .Where(s => s.ProjectId == projectId)
                .ToListAsync();

            return Ok(sprints);
        }
    }
}

