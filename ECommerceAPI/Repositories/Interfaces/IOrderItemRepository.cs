using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItems>> Get();
        Task<IEnumerable<OrderItems>> GetByOrderId(int orderId);
        Task<OrderItems> Get(int id);
        Task<OrderItems> Create(OrderItems orderItems);
        Task Update(OrderItems orderItems);
        Task Delete(int id);
    }
}
