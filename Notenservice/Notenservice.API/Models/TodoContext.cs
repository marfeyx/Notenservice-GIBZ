using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : IdentityDbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; }
        public DbSet<TodoStatus> TodoStatus { get; set; }
        public DbSet<TodoPriority> TodoPriority { get; set; }
    }
}