using ECommerceAPI.Models;
using ECommerceAPI.Models.Contexts;
using ECommerceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceContext _context;

        public UserRepository(ECommerceContext context)
        {
            _context = context;
        }
        public async Task<Users> Create(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Users>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Update(Users user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
