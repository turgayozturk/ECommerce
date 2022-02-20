using ECommerceAPI.Models;
using ECommerceAPI.Models.Contexts;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ECommerceContext _context;
        public OrderItemRepository(ECommerceContext context)
        {
            _context = context;
        }
        public async Task<OrderItems> Create(OrderItems orderItems)
        {
            _context.OrderItems.Add(orderItems);
            await _context.SaveChangesAsync();
            return orderItems;
        }

        public async Task Delete(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItems>> Get()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItems> Get(int id)
        {
            return await _context.OrderItems.FindAsync(id);
        }

        public async Task<IEnumerable<OrderItems>> GetByOrderId(int orderId)
        {
            return await _context.OrderItems.Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task Update(OrderItems orderItems)
        {
            _context.Entry(orderItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
