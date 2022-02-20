using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        public CartsController(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Carts>> GetCarts()
        {
            return await _cartRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carts>> GetCarts(int id)
        {
            return await _cartRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Carts>> PostCart([FromBody] Carts cart)
        {
            //Sipariş oluşturulurken ürün stok kontrolü yapılır
            //Stok sıfır ise (if) hata dönülür
            //Stok Şipariş edilen ürün miktarından az ise (else if) hata dönülür
            //Stok var ise (else) Şipariş edilen ürün miktarı kadar stoktan düşülür

            var product = await _productRepository.Get(cart.ProductId);
            if (product.Stock == 0)
            {
                return BadRequest();
            }
            else if (product.Stock < cart.Quantity)
            {
                return BadRequest();
            }
            else
            {
                product.Stock = product.Stock - cart.Quantity;
                await _productRepository.Update(product);
            }

            var newCart = await _cartRepository.Create(cart);
            return CreatedAtAction(nameof(GetCarts), new { id = newCart.Id }, newCart);
        }

        //[HttpPut]
        //public async Task<ActionResult> PutCarts(int id, [FromBody] Carts cart)
        //{
        //    if (id != cart.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _cartRepository.Update(cart);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cart = await _cartRepository.Get(id);

            if (cart == null)
            {
                return NotFound();
            }

            await _cartRepository.Delete(cart.Id);
            return NoContent();
        }
    }
}
