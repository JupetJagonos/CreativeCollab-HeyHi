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
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetupEvent>>> GetMeetupEvents()
        {
            return await _context.MeetupEvents.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeetupEvent>> GetMeetupEvent(int id)
        {
            var meetupEvent = await _context.MeetupEvents.FindAsync(id);

            if (meetupEvent == null)
            {
                return NotFound();
            }

            return meetupEvent;
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeetupEvent(int id, MeetupEvent meetupEvent)
        {
            if (id != meetupEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(meetupEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetupEventExists(id))
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

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<MeetupEvent>> PostMeetupEvent(MeetupEvent meetupEvent)
        {
            // Handle the YouTubeLinks relationship
            if (meetupEvent.YouTubeLinks != null)
            {
                foreach (var link in meetupEvent.YouTubeLinks)
                {
                    // Ensure each YouTubeLink has a valid MeetupEventId
                    var meetupEventRef = await _context.MeetupEvents.FindAsync(link.MeetupEventId);
                    if (meetupEventRef == null)
                    {
                        return BadRequest("Invalid MeetupEventId in YouTubeLink.");
                    }
                    link.MeetupEvent = meetupEventRef;  // Ensure the link is connected to the correct MeetupEvent
                }
            }

            _context.MeetupEvents.Add(meetupEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeetupEvent", new { id = meetupEvent.Id }, meetupEvent);
        }


        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeetupEvent(int id)
        {
            var meetupEvent = await _context.MeetupEvents.FindAsync(id);
            if (meetupEvent == null)
            {
                return NotFound();
            }

            _context.MeetupEvents.Remove(meetupEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeetupEventExists(int id)
        {
            return _context.MeetupEvents.Any(e => e.Id == id);
        }
    }
}
