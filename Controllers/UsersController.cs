using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Database_Context;
using StoreAPI.Database_Entities;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public UsersController(DatabaseContext context)
        {
            _context = context;
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
        public async Task<ActionResult> PostUser([FromBody] User user)
        {

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
