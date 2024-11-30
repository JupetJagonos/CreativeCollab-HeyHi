using Collab_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Collab_Project.Models;

namespace Collab_Project.Controllers
{
    /// <summary>
    /// Handles CRUD operations for Users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersAPIController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public UsersAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>
        /// 200 OK
        /// [{User},{User}]
        /// </returns>
        /// <example>
        /// GET: api/Usersapi/List -> [{User},{User}]
        /// </example>
        [HttpGet(template: "List")] 
        public async Task<ActionResult<IEnumerable<User>>> ListUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            return (users);
        }

        /// <summary>
        /// Retrieves a user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>
        /// 200 OK
        /// {User}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Usersapi/Find/1 -> {User}
        /// </example>
        [HttpGet(template: "Find/{id}")] 
        public async Task<ActionResult<User>> FindUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return (user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>
        /// 201 Created
        /// Location: api/Usersapi/Find/{id}
        /// {User}
        /// or
        /// 400 Bad Request
        /// </returns>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("FindUser", new { id = user.Id }, user);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpPut(template: "Update/{id}")] 
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _context.Entry(existingUser).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpDelete(template: "Delete/{id}")] 
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Checks if a user with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the user to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
