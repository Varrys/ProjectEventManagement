using BusinessLogic.Context;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ES2DbContext _context;

    public TicketsController(ES2DbContext context)
    {
        _context = context;
    }

    // GET: api/Tickets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
    {
        return await _context.Tickets.ToListAsync();
    }

    // GET: api/Tickets/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Ticket>> GetTicket(Guid id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null) return NotFound();

        return ticket;
    }

    // PUT: api/Tickets/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTicket(Guid id, Ticket ticket)
    {
        if (id != ticket.TicketId) return BadRequest();

        _context.Entry(ticket).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TicketExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Tickets
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTicket", new { id = ticket.TicketId }, ticket);
    }

    // DELETE: api/Tickets/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(Guid id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TicketExists(Guid id)
    {
        return _context.Tickets.Any(e => e.TicketId == id);
    }
}