using System.Linq.Expressions;
using ks.application.Repositories;
using ks.application.Services.Interfaces;
using ks.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ks.infras.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected DbSet<TEntity> dbSet;
    private readonly IClaimsService claimsService;
    public GenericRepository(IServiceProvider serviceProvider)
    {
        dbSet = serviceProvider.GetRequiredService<AppDbContext>().Set<TEntity>();
        claimsService = serviceProvider.GetRequiredService<IClaimsService>();
    }
    public async Task<Guid> CreateAsync(TEntity entity,
        CancellationToken cancellationToken = default)
    {
        entity.CreatedBy = claimsService.CurrentUser;
        await dbSet.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public async Task<List<Guid>> CreateRangeAsync(List<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        List<Guid> guids = [];

        entities.ForEach(x =>
        {
            x.CreatedBy = claimsService.CurrentUser;
            guids.Add(x.Id);
        });
        await dbSet.AddRangeAsync(entities);
        return guids;

    }

    public void SoftRemove(TEntity entity)
    {
        entity.UpdatedBy = claimsService.CurrentUser;
        entity.UpdatedDate = DateTime.Now;
        entity.IsDeleted = true;
        dbSet.Update(entity);
    }

    public void SoftRemoveRange(List<TEntity> entities)
        => entities.ForEach(x =>
        {
            x.UpdatedBy = claimsService.CurrentUser;
            x.UpdatedDate = DateTime.Now;
            x.IsDeleted = true;
            dbSet.Update(x);
        });


    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
            => await includes
                .Aggregate(dbSet.AsQueryable(),
                (entity, property) => entity!.Include(property)).AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync(x => x.IsDeleted == false, cancellationToken);

    public async Task<List<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        => await includes.Aggregate(dbSet.AsQueryable(), (e, p) => e.Include(p))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public void Update(TEntity entity)
    {
        entity.UpdatedBy = claimsService.CurrentUser;
        entity.UpdatedDate = DateTime.Now;
        dbSet.Update(entity);
    }

    public void UpdateRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.UpdatedBy = claimsService.CurrentUser;
            x.UpdatedDate = DateTime.Now;
            dbSet.Update(x);
        });
    }

    public async Task<List<TEntity>?> WhereAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    => await includes.Aggregate(dbSet.AsQueryable(), (e, p) => e.Include(p))
        .AsNoTracking()
        .Where(predicate)
        .Where(x => x.IsDeleted == false)
        .ToListAsync(cancellationToken);
}