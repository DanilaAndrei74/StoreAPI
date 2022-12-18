using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Database_Context;
using StoreAPI.Database.Entities;
using StoreAPI.Database.Validators;
using FluentValidation.Results;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private UserValidator _validator;
        public UsersController(DatabaseContext context, UserValidator validator)
        {
            _context = context;
            _validator = validator;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = _context.Users;

            return Ok(users);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId );

            return Ok(user);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult<string> PostUser([FromBody] User user)
        {
            ValidationResult result = _validator.Validate(user);
            if (!result.IsValid) return ValidationProblem("user not valid");

            var toAdd = new User
            {
                Id = user.Id,
                Name = user.Name
            };

            _context.Users.Add(toAdd);
            _context.SaveChanges();

            return Created("User created", null);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
