using BusinessLogic.Context;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ES2DbContext _context;

    public UsersController(ES2DbContext context)
    {
        _context = context;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<dynamic>>> GetUsers()
    {
        if (_context.Users == null) return NotFound();

        return await _context
            .Users.Select(a => new
            {
                a.UserId,
                a.Email,
                a.Username,
                a.Password,
                a.Name,
                a.Role,
                a.CreatedAt,
                a.Enable
            }).ToListAsync();
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(Guid id)
    {
        if (_context.Users == null) return NotFound();

        var user = await _context.Users.FindAsync(id);

        if (user == null) return NotFound();

        return user;
    }

    // PUT: api/Users/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(Guid id, User user)
    {
        if (id != user.UserId) return BadRequest();

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Users
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        if (_context.Users == null) return Problem("Entity set 'ES2DbContext.Users' is null.");

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        if (_context.Users == null) return NotFound();

        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(Guid id)
    {
        return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
    }
}