using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<Carts>> Get();
        Task<Carts> Get(int id);
        Task<Carts> Create(Carts cart);
        Task Update(Carts cart);
        Task Delete(int id);
    }
}
