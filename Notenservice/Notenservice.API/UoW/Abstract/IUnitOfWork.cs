using API.Models;
using API.Repositories.Abstract;

namespace API.UoW.Abstract;

public interface IUnitOfWork : IDisposable
{
    IRoleRepository Roles { get; }
    IUserRepository Users { get; }
    IRepository<Category> Categories { get; }
    IRepository<Product> Products { get; }
    IRepository<Status> Statuses { get; }
    IOrderRepository Orders { get; }
    IRepository<OrderItem> OrderItems { get; }
    Task<int> CompleteAsync();
}
