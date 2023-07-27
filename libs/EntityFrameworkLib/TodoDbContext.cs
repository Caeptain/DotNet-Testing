using EntityFrameworkLib.Entities;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkLib;
public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoEntity> Todos { get; set; }
}
