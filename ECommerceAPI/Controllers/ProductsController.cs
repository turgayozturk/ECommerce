using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Products>> GetProducts()
        {
            return await _productRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            return await _productRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Products>> PostProduct([FromBody] Products product)
        {
            var newProduct = await _productRepository.Create(product);
            return CreatedAtAction(nameof(GetProducts), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut]
        public async Task<ActionResult> PutProducts(int id, [FromBody] Products product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productRepository.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Delete(product.Id);
            return NoContent();
        }
    }
}
