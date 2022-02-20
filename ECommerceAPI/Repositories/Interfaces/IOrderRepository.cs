using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Orders>> Get();
        Task<Orders> Get(int id);
        Task<Orders> Create(Orders order);
        Task Update(Orders order);
        Task Delete(int id);
    }
}
