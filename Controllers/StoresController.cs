using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Contracts.Data.InputModels;
using StoreAPI.Database.Context;
using StoreAPI.Database.Entities;
using StoreAPI.Database.Validators;
using StoreAPI.Services.Authentication;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class StoresController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private StoreInputValidator _validator;
        private AuthenticationService _authentication;
        public StoresController(DatabaseContext context, StoreInputValidator validator, AuthenticationService authentication)
        {
            _context = context;
            _validator = validator;
            _authentication = authentication;
        }

        [HttpGet]
        public ActionResult<List<StoreOutput>> GetStores()
        {
            var stores = _context.Stores.Where(x => x.IsDeleted == false);
            var output = new List<StoreOutput>();
            foreach (var store in stores)
            {
                output.Add(new StoreOutput
                {
                    Id = store.Id,
                    Name = store.Name,
                    Address = store.Address,
                    Description = store.Description,
                });
            }

            return Ok(output);
        }

        [HttpGet("{storeId}")]
        public async Task<ActionResult<StoreOutput>> GetStoreById(Guid storeId)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(x => x.Id == storeId && x.IsDeleted == false);
            if (store == null) return NotFound();

            var output = new StoreOutput
            {
                Id = store.Id,
                Name = store.Name,
                Address = store.Address,
                Description = store.Description,
            };

            return Ok(output);
        }

        [HttpPost]
        public ActionResult<string> PostStore([FromBody] StoreInput store)
        {
            ValidationResult result = _validator.Validate(store);
            if (!result.IsValid) return ValidationProblem();

            var existsInDb = _context.Stores.FirstOrDefault(x => x.Name == store.Name);
            if (existsInDb != null) return BadRequest("Store already exists");

            var toAdd = new Store
            {
                Name = store.Name,
                Address = store.Address,
                Description = store.Description
            };

            _context.Stores.Add(toAdd);
            _context.SaveChanges();

            return Created("Store created at {toAdd.Id}", toAdd.Id);
        }

        [HttpPut("{storeId}")]
        public ActionResult<StoreOutput> PutStore(Guid storeId, [FromBody] StoreInput store)
        {
            ValidationResult result = _validator.Validate(store);
            if (!result.IsValid) return ValidationProblem();

            var storeFromDb = _context.Stores.FirstOrDefault(x => x.Id == storeId);
            if (storeFromDb == null) return NotFound();

            storeFromDb.Name = store.Name;
            storeFromDb.Address = store.Address;
            storeFromDb.Description = store.Description;
            storeFromDb.IsDeleted = false;

            _context.SaveChanges();

            var toDisplay = new StoreOutput()
            {
                Id = storeId,
                Name = store.Name,
                Address = store.Address,
                Description = store.Description
            };

            return Created("", null);
        }

        [HttpDelete("{storeId}")]
        public ActionResult<Guid> DeleteStore(Guid storeId)
        {
            var store = _context.Stores.FirstOrDefault(x => x.Id == storeId && x.IsDeleted == false);
            if (store == null) return NotFound();

            store.IsDeleted = true;
            _context.SaveChanges();

            return Accepted(storeId.ToString());
        }
    }
}
