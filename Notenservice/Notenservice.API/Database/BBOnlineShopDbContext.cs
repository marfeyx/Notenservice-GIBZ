using Microsoft.EntityFrameworkCore;
using System.Text;

namespace API.db;

public class BBOnlineShopDbContext : DbContext
{
    public BBOnlineShopDbContext(DbContextOptions<BBOnlineShopDbContext> options) : base(options)
    {
        public DbSet<User> Users { get; set; }
    }


    // public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
