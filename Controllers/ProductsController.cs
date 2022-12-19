using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Contracts.Data;
using StoreAPI.Database.Context;
using StoreAPI.Database.Validators;
using StoreAPI.Services.Authentication;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private UserInputValidator _validator;
        private AuthenticationService _authentication;
        public ProductsController(DatabaseContext context, UserInputValidator validator, AuthenticationService authentication)
        {
            _context = context;
            _validator = validator;
            _authentication = authentication;
        }


        [HttpGet]
        public ActionResult<List<ProductOutput>> GetProducts()
        {
            var products = _context.Products.Where(x => x.IsDeleted == false);
            var output = new List<ProductOutput>();
            foreach (var product in products)
            {
                output.Add(new ProductOutput
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                });
            }

            return Ok(output);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductOutput>> GetProductById(Guid productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null) return NotFound();

            var output = new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
            };

            return Ok(output);
        }

        //[HttpPost]
        //public ActionResult<string> PostUser([FromBody] UserInput user)
        //{
        //    ValidationResult result = _validator.Validate(user);
        //    if (!result.IsValid) return ValidationProblem("user not valid");

        //    var existsInDb = _context.Users.FirstOrDefault(x => x.Email == user.Email);
        //    if (existsInDb != null) return BadRequest("Email already in use");

        //    var saltToAdd = _authentication.CreateSalt();
        //    var passwordToAdd = _authentication.HashPassword(user.Password, saltToAdd);

        //    var toAdd = new User
        //    {
        //        Email = user.Email,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Salt = saltToAdd,
        //        Password = passwordToAdd
        //    };

        //    _context.Users.Add(toAdd);
        //    _context.SaveChanges();

        //    return Created("User created at {toAdd.Id}", toAdd.Id);
        //}

        //[HttpPut("{userId}")]
        //public ActionResult<UserOutput> PutUser(Guid userId, [FromBody] UserInput user)
        //{
        //    ValidationResult result = _validator.Validate(user);
        //    if (!result.IsValid) return ValidationProblem("user not valid");

        //    var userFromDb = _context.Users.FirstOrDefault(x => x.Id == userId);
        //    if (userFromDb == null) return NotFound();

        //    userFromDb.Email = user.Email;
        //    userFromDb.FirstName = user.FirstName;
        //    userFromDb.LastName = user.LastName;
        //    userFromDb.Password = _authentication.HashPassword(user.Password, userFromDb.Salt);
        //    userFromDb.IsDeleted = false;

        //    _context.SaveChanges();

        //    var toDisplay = new UserOutput()
        //    {
        //        Id = userId,
        //        Email = user.Email,
        //        LastName = user.LastName,
        //        FirstName = user.FirstName,
        //    };


        //    return Created("", null);
        //}

        //[HttpDelete("{userId}")]
        //public ActionResult<Guid> DeleteUser(Guid userId)
        //{
        //    var user = _context.Users.FirstOrDefault(x => x.Id == userId && x.IsDeleted == false);
        //    if (user == null) return NotFound();

        //    user.IsDeleted = true;
        //    _context.SaveChanges();

        //    return Accepted(userId.ToString());
        //}
    }
}
