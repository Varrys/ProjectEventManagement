/*
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Context;
using BusinessLogic.Entities;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ES2DbContext _context;

        public AuthController(ES2DbContext context)
        {
            _context = context;
        }

        // POST: api/Auth/Login
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = GenerateToken(user);

            var response = new AuthenticationResponse
            {
                Token = token,
                Auth = new AuthDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Role = user.Role
                }
            };

            return Ok(response);
        }

        private string GenerateToken(User user)
        {
            // Implement the logic to generate an authentication token (e.g., using JWT)
            // Return the generated token
            return "your_generated_token";
        }
    }
}
*/

