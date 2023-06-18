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
    public class FeedbacksController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public FeedbacksController(ES2DbContext context)
        {
            _context = context;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        // GET: api/Feedbacks/{feedbackId}
        [HttpGet("{feedbackId}")]
        public async Task<ActionResult<Feedback>> GetFeedback(Guid feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);

            if (feedback == null)
                return NotFound();

            return feedback;
        }

        // PUT: api/Feedbacks/{feedbackId}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{feedbackId}")]
        public async Task<IActionResult> PutFeedback(Guid feedbackId, Feedback feedback)
        {
            if (feedbackId != feedback.FeedbackId)
                return BadRequest();

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(feedbackId))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { feedbackId = feedback.FeedbackId }, feedback);
        }

        // DELETE: api/Feedbacks/{feedbackId}
        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(Guid feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback == null)
                return NotFound();

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackExists(Guid feedbackId)
        {
            return _context.Feedbacks.Any(e => e.FeedbackId == feedbackId);
        }
    }
}
