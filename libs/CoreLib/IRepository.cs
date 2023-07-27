namespace CoreLib;

public interface IRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> AsQueryable { get; }
    Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetAsyc(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}