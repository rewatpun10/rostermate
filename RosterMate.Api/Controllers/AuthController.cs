
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RosterMate.Application.DTOs;
using RosterMate.Application.Interfaces;
using RosterMate.Domain.Helpers;


namespace RossteraMate.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly IConfiguration _config;

        public AuthController(IStaffService staffService, IConfiguration config)
        {
            _staffService = staffService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var staff = await _staffService.GetByEmailAsync(loginRequest.Email);
            if (staff == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var isPasswordValid = PasswordHelper.VerifyPassword(loginRequest.Password, staff.PasswordHash, staff.PasswordSalt);
            if (!isPasswordValid)
            {
                return Unauthorized("Invalid email or password.");
            }

            //Generate JWT token
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("id", staff.Id.ToString()),
                new Claim("email", staff.Email),
                new Claim("role", staff.Role.ToString())

            }, authenticationType: "custom"); ;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["JwtSettings:TokenExpirationInMinutes"])),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                StaffId = staff.Id,
                Email = staff.Email,
                FirstName = staff.FirstName,
                Role = staff.Role.ToString()
            });
        }

    }
}