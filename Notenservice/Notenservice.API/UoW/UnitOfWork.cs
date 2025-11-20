using API.UoW.Abstract;

namespace API.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly BBOnlineShopDbContext _context;

    public UnitOfWork(BBOnlineShopDbContext context)
    {
        _context = context;
        Roles = new RoleRepository(_context);
        Users = new UserRepository(_context);
        Categories = new GenericRepository<Category>(_context);
        Products = new GenericRepository<Product>(_context);
        Statuses = new GenericRepository<Status>(_context);
        Orders = new OrderRepository(_context);
        OrderItems = new GenericRepository<OrderItem>(_context);
    }

    public IRoleRepository Roles { get; private set; }
    public IUserRepository Users { get; private set; }
    public IRepository<Category> Categories { get; private set; }
    public IRepository<Product> Products { get; private set; }
    public IRepository<Status> Statuses { get; private set; }
    public IOrderRepository Orders { get; private set; }
    public IRepository<OrderItem> OrderItems { get; private set; }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
