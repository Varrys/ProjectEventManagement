using BusinessLogic.Context;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public EventsController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetEvents()
        {
            if (_context.Events == null)
                return NotFound();

            return await _context.Events
                .Include(e => e.Tickets)
                .Include(e => e.Activities)
                .Select(a => new
                {
                    a.EventId,
                    a.Name,
                    a.Date,
                    a.Location,
                    a.Description,
                    a.MaxCapacity,
                    a.UserId,
                    Tickets = a.Tickets.Select(b => new
                    {
                        b.TicketId,
                        b.Price,
                        b.Name,
                        b.Description,
                        b.Quantity,
                        b.EventId
                    }),
                    Activities = a.Activities.Select(c => new 
                    {
                        c.ActivityId,
                        c.Name,
                        c.Datetime,
                        c.Description,
                        c.EventId
                    })
                })
                .ToListAsync();
        }


        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null) return NotFound();

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.EventId) return BadRequest();

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Events/search?searchTerm={term}
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<dynamic>>> SearchEvents(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetEvents();
            }

            var filteredEvents = await _context.Events
                .Where(e => e.Name.Contains(searchTerm) || e.Date.ToString().Contains(searchTerm) || e.Location.Contains(searchTerm))
                .Select(a => new
                {
                    a.EventId,
                    a.Name,
                    a.Date,
                    a.Location,
                    a.Description,
                    a.MaxCapacity,
                    a.UserId,
                    Tickets = a.Tickets.Select(b => new
                    {
                        b.TicketId,
                        b.Price,
                        b.Name,
                        b.Description,
                        b.Quantity,
                        b.EventId
                    })
                })
                .ToListAsync();

            return filteredEvents;
        }

        private bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
