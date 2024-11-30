using Collab_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Collab_Project.Models;

namespace Collab_Project.Controllers
{
    /// <summary>
    /// Handles CRUD operations for Events.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsAPIController"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public EventsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of all events.
        /// </summary>
        /// <returns>
        /// 200 OK
        /// </returns>
        /// <example>
        /// GET: api/Eventsapi/List 
        /// </example>
        [HttpGet(template: "List")]
        public async Task<ActionResult<IEnumerable<MeetupEvent>>> ListEvents()
        {
            // Retrieve all events from the database
            List<MeetupEvent> events = await _context.MeetupEvents.ToListAsync();
            return Ok(events);
        }

        /// <summary>
        /// Retrieves an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to retrieve.</param>
        /// <returns>
        /// 200 OK
        /// {Event}
        /// or
        /// 404 Not Found
        /// </returns>
        /// <example>
        /// GET: api/Eventsapi/Find/1 -> {Event}
        /// </example>
        [HttpGet(template: "Find/{id}")]
        public async Task<ActionResult<MeetupEvent>> FindEvent(int id)
        {
            // Attempt to find the event by its ID
            var @event = await _context.MeetupEvents.FindAsync(id);
            if (@event == null)
            {
                // Return 404 Not Found if the event is not found
                return NotFound();
            }
            return (@event);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="event">The event to create.</param>
        /// <returns>
        /// 201 Created
        /// Location: api/Eventsapi/Find/{id}
        /// {Event}
        /// or
        /// 400 Bad Request
        /// </returns>
        [HttpPost(template: "Add")]
        public async Task<ActionResult<MeetupEvent>> AddEvent(MeetupEvent @event)
        {
            // Add the event to the database
            _context.MeetupEvents.Add(@event);
            await _context.SaveChangesAsync();
            // Return 201 Created with the location of the new event
            return CreatedAtAction("FindEvent", new { id = @event.Id }, @event);
        }

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="id">The ID of the event to update.</param>
        /// <param name="event">The updated event.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 400 Bad Request
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpPut(template: "Update/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, MeetupEvent @event)
        {
            // Check if the ID in the URL matches the ID in the event
            if (id != @event.Id)
            {
                // Return 400 Bad Request if the IDs do not match
                return BadRequest();
            }

            // Attempt to find the event by its ID
            var existingEvent = await _context.MeetupEvents.FindAsync(id);
            if (existingEvent == null)
            {
                // Return 404 Not Found if the event is not found
                return NotFound();
            }

            // Update the event
            _context.Entry(existingEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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
        /// Deletes an event by its ID.
        /// </summary>
        /// <param name="id">The ID of the event to delete.</param>
        /// <returns>
        /// 204 No Content
        /// or
        /// 404 Not Found
        /// </returns>
        [HttpDelete(template: "Delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            // Attempt to find the event by its ID
            var @event = await _context.MeetupEvents.FindAsync(id);
            if (@event == null)
            {
                // Return 404 Not Found if the event is not found
                return NotFound();
            }

            // Remove the event from the database
            _context.MeetupEvents.Remove(@event);
            await _context.SaveChangesAsync();
            // Return 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Checks if an event with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the event to check.</param>
        /// <returns>True if the event exists, false otherwise.</returns>
        private bool EventExists(int id)
        {
            return _context.MeetupEvents.Any(e => e.Id == id);
        }
    }
}
