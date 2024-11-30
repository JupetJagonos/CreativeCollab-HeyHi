using Collab_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Collab_Project.Models;

namespace Collab_Project.Controllers
{
    /// <summary>
    /// Handles CRUD operations for YouTube links.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeLinksAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubeLinksAPIController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public YouTubeLinksAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all YouTube links.
        /// </summary>
        /// <returns>
        /// 200 OK
        /// [{YouTubeLink},{YouTubeLink}]
        /// </returns>
        /// <example>
        /// GET: api/YouTubeLinksapi/List -> [{YouTubeLink},{YouTubeLink}]
        /// </example>
        [HttpGet(template: "List")] 
        public async Task<ActionResult<IEnumerable<YouTubeLink>>> ListYouTubeLinks()
        {
            List<YouTubeLink> youTubeLinks = await _context.YouTubeLinks.ToListAsync();
            return Ok(youTubeLinks);
        }

        /// <summary>
        /// Retrieves a YouTube link by its ID.
        /// </summary>
        /// <param name="id">The ID of the YouTube link to retrieve.</param>
        /// <returns>
        /// 200 OK
        /// {YouTubeLink}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/YouTubeLinksapi/Find/1 -> {YouTubeLink}
        /// </example>
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<YouTubeLink>> FindYouTubeLink(int id)
        {
            var youTubeLink = await _context.YouTubeLinks.FindAsync(id);
            if (youTubeLink == null)
            {
                return NotFound();
            }
            return Ok(youTubeLink);
        }

        /// <summary>
        /// Creates a new YouTube link.
        /// </summary>
        /// <param name="youTubeLink">The YouTube link to create.</param>
        /// <returns>
        /// 201 Created
        /// Location: api/YouTubeLinksapi/Find/{id}
        /// {YouTubeLink}
        /// or
        /// 400 Bad Request
        /// </returns>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<YouTubeLink>> AddYouTubeLink(YouTubeLink youTubeLink)
        {
            _context.YouTubeLinks.Add(youTubeLink);
            await _context.SaveChangesAsync();
            return CreatedAtAction("FindYouTubeLink", new { id = youTubeLink.Id }, youTubeLink);
        }

        /// <summary>
        /// Updates an existing YouTube link.
        /// </summary>
        /// <param name="id">The ID of the YouTube link to update.</param>
        /// <param name="youTubeLink">The updated YouTube link.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpPut(template: "Update/{id}")]
        public async Task<IActionResult> UpdateYouTubeLink(int id, YouTubeLink youTubeLink)
        {
            if (id != youTubeLink.Id)
            {
                return BadRequest();
            }

            var existingYouTubeLink = await _context.YouTubeLinks.FindAsync(id);
            if (existingYouTubeLink == null)
            {
                return NotFound();
            }

            _context.Entry(existingYouTubeLink).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YouTubeLinkExists(id))
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
        /// Deletes a YouTube link by its ID.
        /// </summary>
        /// <param name="id">The ID of the YouTube link to delete.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpDelete(template: "Delete/{id}")]
        public async Task<IActionResult> DeleteYouTubeLink(int id)
        {
            var youTubeLink = await _context.YouTubeLinks.FindAsync(id);
            if (youTubeLink == null)
            {
                return NotFound();
            }
            _context.YouTubeLinks.Remove(youTubeLink);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Checks if a YouTube link with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the YouTube link to check.</param>
        /// <returns>True if the YouTube link exists, false otherwise.</returns>
        private bool YouTubeLinkExists(int id)
        {
            return _context.YouTubeLinks.Any(e => e.Id == id);
        }
    }
}
