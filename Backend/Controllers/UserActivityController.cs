using BusinessLogic.Context;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public UserActivitiesController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/UserActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserActivity>>> GetUserActivities()
        {
            return await _context.UserActivities.ToListAsync();
        }

        // GET: api/UserActivities/{eventId}/{activityId}
        [HttpGet("{eventId}/{activityId}")]
        public async Task<ActionResult<UserActivity>> GetUserActivity(Guid eventId, Guid activityId)
        {
            var userActivity = await _context.UserActivities.FindAsync(eventId, activityId);

            if (userActivity == null)
                return NotFound();

            return userActivity;
        }

        // PUT: api/UserActivities/{eventId}/{activityId}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{eventId}/{activityId}")]
        public async Task<IActionResult> PutUserActivity(Guid eventId, Guid activityId, UserActivity userActivity)
        {
            if (eventId != userActivity.EventId || activityId != userActivity.ActivityId)
                return BadRequest();

            _context.Entry(userActivity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserActivityExists(eventId, activityId))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // POST: api/UserActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserActivity>> PostUserActivity(UserActivity userActivity)
        {
            _context.UserActivities.Add(userActivity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserActivity", new { eventId = userActivity.EventId, activityId = userActivity.ActivityId },
                userActivity);
        }

        // DELETE: api/UserActivities/{eventId}/{activityId}
        [HttpDelete("{eventId}/{activityId}")]
        public async Task<IActionResult> DeleteUserActivity(Guid eventId, Guid activityId)
        {
            var userActivity = await _context.UserActivities.FindAsync(eventId, activityId);
            if (userActivity == null)
                return NotFound();

            _context.UserActivities.Remove(userActivity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserActivityExists(Guid eventId, Guid activityId)
        {
            return _context.UserActivities.Any(e => e.EventId == eventId && e.ActivityId == activityId);
        }
    }
}
