using BugTrackingSystem.Data;
using BugTrackingSystem.Dto;
using BugTrackingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Controllers
{
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpPost("api/projects")]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto createProjectDto)
        {
            if (createProjectDto == null || string.IsNullOrEmpty(createProjectDto.Name) || string.IsNullOrEmpty(createProjectDto.Description))
            {
                return BadRequest("Project name and description are required.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized("You must be logged in to create a project.");
            }

            var project = new Project
            {
                Name = createProjectDto.Name,
                Description = createProjectDto.Description,
                ApplicationUserId = currentUser.Id,
            };

            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
                var projectMember = new ProjectMember
                {
                    ProjectId = project.Id,
                    UserId = currentUser.Id,
                    Role = RoleType.ProductOwner
                };
                _context.ProjectMembers.Add(projectMember);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateProject), new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        } 
        [HttpGet("api/getallprojects")]
        public IActionResult GetAllProjects()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;  // Get the current user
            if (currentUser == null)
            {
                return Unauthorized("You must be logged in to view projects.");
            }

            // Fetch projects based on the current user's involvement (e.g., working on or watching projects)
            var projects = _context.Projects
                .Where(p => p.ApplicationUserId == currentUser.Id)  // Filter by the current user's ID
                .ToList();

            return Ok(projects);
        }

        [HttpGet("api/project/{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _context.Projects
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }


        [HttpDelete("api/project/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound("Project not found.");
            }
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Project deleted successfully." });
        }



    }
}


  
