using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Contracts.Data;
using StoreAPI.Contracts.Data.InputModels;
using StoreAPI.Contracts.Data.OutputModels;
using StoreAPI.Database.Context;
using StoreAPI.Database.Entities;
using StoreAPI.Database.Validators;
using StoreAPI.Services.Authentication;

namespace StoreAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsInStoresController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private ProductsInStoresInputValidator _validator;
        private AuthenticationService _authentication;

        public ProductsInStoresController(DatabaseContext context, ProductsInStoresInputValidator validator, AuthenticationService authentication)
        {
            _context = context;
            _validator = validator;
            _authentication = authentication;
        }

        [HttpGet]
        public ActionResult<List<ProductInStoreOutput>> GetProductsInStores()
        {
            var productsInStores = _context.ProductsInStores.Where(x => x.IsDeleted == false);
            var output = new List<ProductInStoreOutput>();
            foreach (var productInStore in productsInStores)
            {
                output.Add(new ProductInStoreOutput
                {
                    StoreId = productInStore.StoreId,
                    ProductId = productInStore.ProductId,
                    Quantity = productInStore.Quantity
                });
            }

            return Ok(output);
        }

        [HttpGet("QueryWithProductId/{productId}")]
        public ActionResult<List<ProductInStoreOutput>> GetByProductsId(Guid productId)
        {
            IEnumerable<ProductInStore> productsInStores = _context.ProductsInStores
                .Where(x => x.ProductId == productId && x.IsDeleted == false);
            var output = new List<ProductInStoreOutput>();
            if (productsInStores == null) return Ok(output);

            foreach (var productInStore in productsInStores)
                output.Add(new ProductInStoreOutput
                {
                    StoreId = productInStore.StoreId,
                    ProductId = productInStore.ProductId,
                    Quantity = productInStore.Quantity,
                });

            return Ok(output);
        }

        [HttpGet("QueryWithStoreId/{storeId}")]
        public ActionResult<List<ProductInStoreOutput>> GetByStoreId(Guid storeId)
        {
            IEnumerable<ProductInStore> productsInStores = _context.ProductsInStores
                .Where(x => x.StoreId == storeId && x.IsDeleted == false);
            var output = new List<ProductInStoreOutput>();
            if (productsInStores == null) return Ok(output);

            foreach (var productInStore in productsInStores)
                output.Add(new ProductInStoreOutput
                {
                    StoreId = productInStore.StoreId,
                    ProductId = productInStore.ProductId,
                    Quantity = productInStore.Quantity,
                });

            return Ok(output);
        }

        [HttpGet("QueryWithKeys")]
        public ActionResult<ProductInStoreOutput> GetProductInStore([FromBody] ProductInStoreKeyInput productInStoreKeys)
        {
            var productInStore = _context.ProductsInStores
                .FirstOrDefault(x => x.ProductId == productInStoreKeys.ProductId 
                && x.StoreId == productInStoreKeys.StoreId && x.IsDeleted == false);
            if (productInStore == null) return NotFound();

            var output = new ProductInStoreOutput
            {
                StoreId = productInStore.StoreId,
                ProductId = productInStore.ProductId,
                Quantity = productInStore.Quantity,
            };

            return Ok(output);
        }

        [HttpPost]
        public ActionResult<string> PostProductInStore([FromBody] ProductInStoreInput productInStore)
        {
            ValidationResult result = _validator.Validate(productInStore);
            if (!result.IsValid) return ValidationProblem();

            var existsInDb = _context.ProductsInStores
                .FirstOrDefault(x => x.StoreId == productInStore.StoreId && x.ProductId == productInStore.ProductId);
            if (existsInDb != null) return BadRequest("Product already exists");

            var store = _context.Stores.FirstOrDefault(x => x.Id == productInStore.StoreId);
            var product = _context.Products.FirstOrDefault(x => x.Id == productInStore.ProductId);

            if (store == null || product == null) return BadRequest();

            var toAdd = new ProductInStore
            {
                ProductId = productInStore.ProductId,
                StoreId = productInStore.StoreId,
                Quantity = productInStore.Quantity,
                Store = store,
                Product = product
            };

            _context.ProductsInStores.Add(toAdd);
            _context.SaveChanges();

            return Created("ProductInStore created", null); ;
        }

        [HttpPut]
        public ActionResult<ProductInStoreOutput> PutProduct([FromBody] ProductInStoreInput productInStore)
        {
            ValidationResult result = _validator.Validate(productInStore);
            if (!result.IsValid) return ValidationProblem();

            var productInStoreFromDb = _context.ProductsInStores
                .FirstOrDefault(x => x.StoreId == productInStore.StoreId && x.ProductId == productInStore.ProductId);
            if (productInStoreFromDb == null) return NotFound();

            productInStoreFromDb.Quantity = productInStore.Quantity;
            productInStoreFromDb.IsDeleted = false;

            _context.SaveChanges();

            var toDisplay = new ProductInStoreOutput()
            {
                StoreId = productInStore.StoreId,
                ProductId = productInStore.ProductId,
                Quantity = productInStore.Quantity,
            };

            return Created("", null);
        }

        [HttpDelete]
        public ActionResult<Guid> DeleteProduct([FromBody] ProductInStoreKeyInput productInStore)
        {
            var product = _context.ProductsInStores
                .FirstOrDefault(x => x.ProductId == productInStore.ProductId && x.IsDeleted == false && x.StoreId == productInStore.StoreId);
            if (product == null) return NotFound();

            product.IsDeleted = true;
            _context.SaveChanges();

            var toDisplay = productInStore.StoreId.ToString() + "\n" + productInStore.ProductId.ToString();
            return Accepted(toDisplay);
        }

    }
}
