using CoreLib;

using EntityFrameworkLib.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkLib;
public static class ServiceExtension
{
    public static IServiceCollection AddEntityFrameworkDatabase(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<TodoDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        service.Configure<RepositoryOptions>(options => { options.Context = typeof(TodoDbContext); });
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return service;
    }
}
