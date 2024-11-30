using Collab_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Collab_Project.Data;
using Collab_Project.Models;

namespace YourProjectName.Controllers
{
    /// <summary>
    /// Handles CRUD operations for Groups.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupsController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public GroupsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all groups.
        /// </summary>
        /// <returns>
        /// 200 OK
        /// [{Group},{Group}]
        /// </returns>
        /// <example>
        /// GET: api/Groupsapi/List -> [{Group},{Group}]
        /// </example>
        [HttpGet(template: "List")]
        public async Task<ActionResult<IEnumerable<Group>>> ListGroups()
        {
            // Retrieve all groups from the database
            List<Group> groups = await _context.Groups.ToListAsync();
            return Ok(groups);
        }

        /// <summary>
        /// Retrieves a group by its ID.
        /// </summary>
        /// <param name="id">The ID of the group to retrieve.</param>
        /// <returns>
        /// 200 OK
        /// {Group}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Groupsapi/Find/1 -> {Group}
        /// </example>
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<Group>> FindGroup(int id)
        {
            // Attempt to find the group by its ID
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                // Return 404 Not Found if the group is not found
                return NotFound();
            }
            return (group);
        }

        /// <summary>
        /// Creates a new group.
        /// </summary>
        /// <param name="group">The group to create.</param>
        /// <returns>
        /// 201 Created
        /// Location: api/Groupsapi/Find/{id}
        /// {Group}
        /// or
        /// 400 Bad Request
        /// </returns>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<Group>> AddGroup(Group group)
        {
            // Add the group to the database
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            // Return 201 Created with the location of the new group
            return CreatedAtAction("FindGroup", new { id = group.Id }, group);
        }

        /// <summary>
        /// Updates an existing group.
        /// </summary>
        /// <param name="id">The ID of the group to update.</param>
        /// <param name="group">The updated group.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpPut(template: "Update/{id}")]
        public async Task<IActionResult> UpdateGroup(int id, Group group)
        {
            // Check if the ID in the URL matches the ID in the group
            if (id != group.Id)
            {
                // Return 400 Bad Request if the IDs do not match
                return BadRequest();
            }
            // Attempt to find the group by its ID
            var existingGroup = await _context.Groups.FindAsync(id);
            if (existingGroup == null)
            {
                // Return 404 Not Found if the group is not found
                return NotFound();
            }
            // Update the group
            _context.Entry(existingGroup).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Return 204 No Content
            return NoContent();
        }
        /// <summary>
        /// Deletes a group by its ID.
        /// </summary>
        /// <param name="id">The ID of the group to delete.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpDelete(template: "Delete/{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            // Attempt to find the group by its ID
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                // Return 404 Not Found if the group is not found
                return NotFound();
            }
            // Remove the group from the database
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            // Return 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Checks if a group with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the group to check.</param>
        /// <returns>True if the group exists, false otherwise.</returns>
        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }

}
