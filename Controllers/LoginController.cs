using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Contracts.Data;
using StoreAPI.Database.Context;
using StoreAPI.Database.Validators;
using StoreAPI.Services.Authentication;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private LoginValidator _validator;
        private AuthenticationService _authentication;

        public LoginController(DatabaseContext context, LoginValidator validator, AuthenticationService authentication)
        {
            _context = context;
            _validator = validator;
            _authentication = authentication;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> DoLogin([FromBody] LoginCredentials credentials)
        {
            //Aplica validator 
            ValidationResult result = _validator.Validate(credentials);
            if (!result.IsValid) return ValidationProblem("Invalid credentials");

            //Cauta user in context daca exista
            var user = _context.Users.FirstOrDefault(x => x.Email == credentials.Email && x.IsDeleted == false);
            if (user == null) return NotFound();

            //Ia saltul de la user si da hash la parola
            string hashedPassword = _authentication.HashPassword(credentials.Password, user.Salt);
            if (user.Password != hashedPassword) Unauthorized();

            //Return ok si creeaza token
            return Ok();
        }
    }
}
