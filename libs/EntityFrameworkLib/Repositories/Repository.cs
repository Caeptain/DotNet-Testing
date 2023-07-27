using CoreLib;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EntityFrameworkLib.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public IQueryable<TEntity> AsQueryable => _dbSet.AsQueryable();

    public Repository(IServiceProvider serviceProvider, IOptions<RepositoryOptions> options)
    {
        _context = (DbContext)serviceProvider.GetRequiredService(options.Value.Context);
        _dbSet = _context.Set<TEntity>();
    }
    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var result = await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    public async Task<TEntity?> GetAsyc(Guid id)
    {
        var result = await _dbSet.FindAsync(id);
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}