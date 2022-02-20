using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> Get();
        Task<Products> Get(int id);
        Task<Products> Create(Products product);
        Task Update(Products product);
        Task Delete(int id);
    }
}
