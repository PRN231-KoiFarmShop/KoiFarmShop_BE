using System.Linq.Expressions;
using ks.domain.Entities;

namespace ks.application.Repositories;
public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>?> WhereAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<Guid> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<List<Guid>> CreateRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void UpdateRange(List<TEntity> entities);
    void SoftRemove(TEntity entity);
    void SoftRemoveRange(List<TEntity> entities);
}