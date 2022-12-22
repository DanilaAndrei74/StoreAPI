using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Database.Entities;
using StoreAPI.Database.Validators;
using FluentValidation.Results;
using StoreAPI.Database.Context;
using StoreAPI.Contracts.Data;
using StoreAPI.Services.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private UserInputValidator _validator;
        private AuthenticationService _authentication;
        public UsersController(DatabaseContext context, UserInputValidator validator, AuthenticationService authentication)
        {
            _context = context;
            _validator = validator;
            _authentication = authentication;
        }


        [HttpGet]
        public async Task<ActionResult<List<UserOutput>>> GetUsers()
        {
            var users = _context.Users.Where(x => x.IsDeleted == false);
            var output = new List<UserOutput>();
            foreach(var user in users)
            {
                output.Add(new UserOutput
                {   
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                });
            }

            return Ok(output);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserOutput>> GetUserById(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == false);
            if (user == null) return NotFound();
            
            var output = new UserOutput
                { 
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<string> PostUser([FromBody] UserInput user)
        {
            ValidationResult result = _validator.Validate(user);
            if (!result.IsValid) return ValidationProblem("user not valid");

            var existsInDb = _context.Users.FirstOrDefault(x => x.IsDeleted == false && x.Email == user.Email);
            if (existsInDb != null) return BadRequest("Email already in use");

            var saltToAdd = _authentication.CreateSalt();
            var passwordToAdd = _authentication.HashPassword(user.Password, saltToAdd);

            var toAdd = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Salt = saltToAdd,
                Password = passwordToAdd
            };

            _context.Users.Add(toAdd);
            _context.SaveChanges();

            return Created("User created at {toAdd.Id}", toAdd.Id);
        }

        [HttpPut("{userId}")]
        public ActionResult<UserOutput> PutUser(Guid userId, [FromBody] UserInput user)
        {
            ValidationResult result = _validator.Validate(user);
            if (!result.IsValid) return ValidationProblem("user not valid");

            var userFromDb = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (userFromDb == null) return NotFound();

            userFromDb.Email = user.Email;
            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.Password = _authentication.HashPassword(user.Password, userFromDb.Salt);
            userFromDb.IsDeleted = false;

            _context.SaveChanges();

            var toDisplay = new UserOutput()
            {
                Id = userId,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
            };


            return Created("", null);
        }

        [HttpDelete("{userId}")]
        public ActionResult<Guid> DeleteUser(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId && x.IsDeleted == false);
            if(user == null) return NotFound();

            user.IsDeleted = true;
            _context.SaveChanges();

            return Accepted(userId.ToString());
        }
    }
}
