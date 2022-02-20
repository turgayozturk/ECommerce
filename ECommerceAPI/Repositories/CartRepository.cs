using ECommerceAPI.Models;
using ECommerceAPI.Models.Contexts;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceContext _context;
        public CartRepository(ECommerceContext context)
        {
            _context = context;
        }
        public async Task<Carts> Create(Carts cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task Delete(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Carts>> Get()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Carts> Get(int id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public async Task Update(Carts cart)
        {
            _context.Entry(cart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
