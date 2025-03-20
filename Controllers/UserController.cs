//using BugTrackingSystem.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
//using System.Threading.Tasks;
//using BugTrackingSystem.Data;
//using Microsoft.EntityFrameworkCore;

//namespace BugTrackingSystem.Controllers
//{
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly ApplicationDbContext _context;

//        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
//        {
//            _userManager = userManager;
//            _context = context;
//        }
//        [HttpGet("api/all-users")]
//        public async Task<IActionResult> GetAllUsers()
//        {
//            // Query only the Email field for all users
//            var users = await _context.Users
//                .Select(u => new { u.Email })  // Only return Email
//                .ToListAsync();

//            return Ok(users);
//        }
//        // Endpoint to search users by email
//        [HttpGet("api/search-users")]
//        public async Task<IActionResult> SearchUsers([FromQuery] string query)
//        {
//            if (string.IsNullOrEmpty(query))
//            {
//                return BadRequest(new { message = "Query is required" });
//            }

//            var users = await _context.Users
//                .Where(u => u.Email.Contains(query))  // Match email
//                .Take(10)  // Limit to top 10 matches
//                .Select(u => new { u.Email, u.Id })  // Return only necessary fields
//                .ToListAsync();

//            return Ok(users);
//        }

//    }

//    }


