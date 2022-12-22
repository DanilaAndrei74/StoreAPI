using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Contracts.Data;
using StoreAPI.Database.Context;
using StoreAPI.Database.Validators;
using StoreAPI.Services.Authentication;
using StoreAPI.Services.JWT;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private LoginValidator _validator;
        private AuthenticationService _authentication;
        private JwtService _jwtService;

        public LoginController(DatabaseContext context, LoginValidator validator, AuthenticationService authentication, JwtService jwtService)
        {
            _context = context;
            _validator = validator;
            _authentication = authentication;
            _jwtService = jwtService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<string>> DoLogin([FromBody] LoginCredentials credentials)
        {
            ValidationResult result = _validator.Validate(credentials);
            if (!result.IsValid) return ValidationProblem("Invalid credentials");

            var user = _context.Users.FirstOrDefault(x => x.Email == credentials.Email && x.IsDeleted == false);
            if (user == null) return NotFound();

            byte[] hashedPassword = _authentication.HashPassword(credentials.Password, user.Salt);
            if (!_authentication.ValidatePassword(hashedPassword, user.Password)) return Unauthorized("Wrong credentials");

            return Ok(_jwtService.GenerateJSONWebToken(user));
        }

        [HttpPost("GenerateToken")]
        public async Task<ActionResult<String>> GenerateToken([FromBody] string user)
        {
            var a = await _context.Users.FirstOrDefaultAsync(x => x.Email == user);
            return Ok(_jwtService.GenerateJSONWebToken(a));
        }
    }
}
