using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Collab_Project.Data;
using Collab_Project.Models;

namespace Collab_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeLinksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public YouTubeLinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/YouTubeLinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YouTubeLink>>> GetYouTubeLinks()
        {
            return await _context.YouTubeLinks.ToListAsync();
        }

        // GET: api/YouTubeLinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YouTubeLink>> GetYouTubeLink(int id)
        {
            var youTubeLink = await _context.YouTubeLinks.FindAsync(id);
            if (youTubeLink == null)
            {
                return NotFound();
            }
            return youTubeLink;
        }

        // PUT: api/YouTubeLinks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYouTubeLink(int id, YouTubeLink youTubeLink)
        {
            if (id != youTubeLink.Id)
            {
                return BadRequest();
            }
            _context.Entry(youTubeLink).State = EntityState.Modified;
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

        // POST: api/YouTubeLinks
        [HttpPost]
        public async Task<ActionResult<YouTubeLink>> PostYouTubeLink(YouTubeLink youTubeLink)
        {
            var meetupEvent = await _context.MeetupEvents.FindAsync(youTubeLink.MeetupEventId);
            if (meetupEvent == null)
            {
                return BadRequest("MeetupEventId does not exist.");
            }
            _context.YouTubeLinks.Add(youTubeLink);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetYouTubeLink", new { id = youTubeLink.Id }, youTubeLink);
        }

        // DELETE: api/YouTubeLinks/5
        [HttpDelete("{id}")]
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

        private bool YouTubeLinkExists(int id)
        {
            return _context.YouTubeLinks.Any(e => e.Id == id);
        }
    }
}

