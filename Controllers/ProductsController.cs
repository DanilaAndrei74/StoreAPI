using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Contracts.Data;
using StoreAPI.Database.Context;
using StoreAPI.Database.Entities;
using StoreAPI.Database.Validators;
using StoreAPI.Services.Authentication;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private ProductInputValidator _validator;
        private AuthenticationService _authentication;
        public ProductsController(DatabaseContext context, ProductInputValidator validator, AuthenticationService authentication)
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
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId && x.IsDeleted == false);
            if (product == null) return NotFound();

            var output = new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
            };

            return Ok(output);
        }

        [HttpPost]
        public ActionResult<string> PostProduct([FromBody] ProductInput product)
        {
            ValidationResult result = _validator.Validate(product);
            if (!result.IsValid) return ValidationProblem();

            var existsInDb = _context.Products.FirstOrDefault(x => x.Name == product.Name);
            if (existsInDb != null) return BadRequest("Product already exists");

            var toAdd = new Product
            {
                Name = product.Name,
                Description = product.Description
            };

            _context.Products.Add(toAdd);
            _context.SaveChanges();

            return Created("Product created at {toAdd.Id}", toAdd.Id);
        }

        [HttpPut("{productId}")]
        public ActionResult<ProductOutput> PutProduct(Guid productId, [FromBody] ProductInput product)
        {
            ValidationResult result = _validator.Validate(product);
            if (!result.IsValid) return ValidationProblem();

            var productFromDb = _context.Products.FirstOrDefault(x => x.Id == productId);
            if (productFromDb == null) return NotFound();

            productFromDb.Name= product.Name;
            productFromDb.Description= product.Description;
            productFromDb.IsDeleted = false;

            _context.SaveChanges();

            var toDisplay = new ProductOutput()
            {
                Id = productId,
                Name = product.Name,
                Description = product.Description
            };

            return Created("", null);
        }

        [HttpDelete("{productId}")]
        public ActionResult<Guid> DeleteProduct(Guid productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == productId && x.IsDeleted == false);
            if (product == null) return NotFound();

            product.IsDeleted = true;
            _context.SaveChanges();

            return Accepted(productId.ToString());
        }
    }
}
