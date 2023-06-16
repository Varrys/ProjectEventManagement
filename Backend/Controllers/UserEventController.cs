using BusinessLogic.Context;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserEventsController : ControllerBase
{
    private readonly ES2DbContext _context;

    public UserEventsController(ES2DbContext context)
    {
        _context = context;
    }

    // GET: api/UserEvents
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEvent>>> GetUserEvents()
    {
        return await _context.UserEvents.ToListAsync();
    }

    // GET: api/UserEvents/5
    [HttpGet("{eventId}/{userId}")]
    public async Task<ActionResult<UserEvent>> GetUserEvent(Guid eventId, Guid userId)
    {
        var userEvent = await _context.UserEvents.FindAsync(eventId, userId);

        if (userEvent == null) return NotFound();

        return userEvent;
    }

    // PUT: api/UserEvents/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{eventId}/{userId}")]
    public async Task<IActionResult> PutUserEvent(Guid eventId, Guid userId, UserEvent userEvent)
    {
        if (eventId != userEvent.EventId || userId != userEvent.UserId) return BadRequest();

        _context.Entry(userEvent).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserEventExists(eventId, userId))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/UserEvents
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<UserEvent>> PostUserEvent(UserEvent userEvent)
    {
        _context.UserEvents.Add(userEvent);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUserEvent", new { eventId = userEvent.EventId, userId = userEvent.UserId },
            userEvent);
    }

    // DELETE: api/UserEvents/5
    [HttpDelete("{eventId}/{userId}")]
    public async Task<IActionResult> DeleteUserEvent(Guid eventId, Guid userId)
    {
        var userEvent = await _context.UserEvents.FindAsync(eventId, userId);
        if (userEvent == null) return NotFound();

        _context.UserEvents.Remove(userEvent);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserEventExists(Guid eventId, Guid userId)
    {
        return _context.UserEvents.Any(e => e.EventId == eventId && e.UserId == userId);
    }
}