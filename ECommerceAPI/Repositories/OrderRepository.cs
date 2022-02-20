using ECommerceAPI.Models;
using ECommerceAPI.Models.Contexts;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceContext _context;

        public OrderRepository(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<Orders> Create(Orders order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Orders>> Get()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Orders> Get(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task Update(Orders order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
