using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> Get();
        Task<Users> Get(int id);
        Task<Users> Create(Users user);
        Task Update(Users user);
        Task Delete(int id);
    }
}
