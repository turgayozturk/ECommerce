using ECommerceAPI.Models;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        public OrdersController(
            IOrderRepository orderRepository, 
            IProductRepository productRepository,
            IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Orders>> GetOrders()
        {
            return await _orderRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            return await _orderRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrder([FromBody] OrderWithOrderItems order)
        {
            //Sipariş oluşturulurken ürün stok kontrolü yapılır
            //Stok sıfır ise (if) hata dönülür
            //Stok Şipariş edilen ürün miktarından az ise (else if) hata dönülür
            //Stok var ise (else) Şipariş edilen ürün miktarı kadar stoktan düşülür
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.Get(item.Id);
                if (product.Stock == 0)
                {
                    return BadRequest();
                }
                else if (product.Stock < item.Quantity)
                {
                    return BadRequest();
                }
                else
                {
                    product.Stock = product.Stock - item.Quantity;
                    await _productRepository.Update(product);

                    await _orderItemRepository.Create(item);
                }
            }

            var newOrder = await _orderRepository.Create(order.Orders);

            return CreatedAtAction(nameof(GetOrders), new { id = newOrder.Id }, newOrder);
        }

        //[HttpPut]
        //public async Task<ActionResult> PutOrders(int id, [FromBody] Orders order)
        //{
        //    if (id != order.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _orderRepository.Update(order);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _orderRepository.Get(id);

            if (order == null)
            {
                return NotFound();
            }
            //Silinen Sipariş içindeki miktarlar ürünlere geri aktarılır
            var orderItems = await _orderItemRepository.GetByOrderId(order.Id);

            foreach (var item in orderItems)
            {
                var product = await _productRepository.Get(item.ProductId);

                product.Stock = product.Stock + item.Quantity;
                await _productRepository.Update(product);
            }

            await _orderRepository.Delete(order.Id);
            return NoContent();
        }
    }
}
