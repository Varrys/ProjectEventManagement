using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ES2DbContext _context;

        public AuthController(ES2DbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Auth/Login
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == request.Username && u.Password == request.Password);

            if (user == null) return Unauthorized();

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

        // POST: api/Auth/Register
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(RegistrationRequest request)
        {
            var newUser = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Name = request.Name,
                Role = "User"
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var token = GenerateToken(newUser);

            var response = new AuthenticationResponse
            {
                Token = token,
                Auth = new AuthDto
                {
                    UserId = newUser.UserId,
                    Username = newUser.Username,
                    Role = newUser.Role
                }
            };

            return Ok(response);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
