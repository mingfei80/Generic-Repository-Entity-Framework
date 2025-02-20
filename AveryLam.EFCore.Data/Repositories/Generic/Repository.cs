using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AveryLam.EFCore.Data.Repositories.Generic;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AveryLamDbContext _averyLamDbContext;

    public Repository(AveryLamDbContext averyLamDbContext)
    {
        _averyLamDbContext = averyLamDbContext;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _averyLamDbContext.Set<TEntity>().AddAsync(entity);
        await _averyLamDbContext.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entities)
    {
        await _averyLamDbContext.Set<TEntity>().AddRangeAsync(entities);
        await _averyLamDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _averyLamDbContext.Set<TEntity>().Remove(entity);
        await _averyLamDbContext.SaveChangesAsync();
    }

    public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await FindAsync(predicate);
        _averyLamDbContext.Set<TEntity>().RemoveRange(entities); 
        await _averyLamDbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
                                            bool asNoTracking = true,
                                            Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        return await Get(asNoTracking, include).FirstOrDefaultAsync(predicate);
    }


    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate,
                                                bool asNoTracking = true,
                                                Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        return await Get(asNoTracking, include).Where(predicate).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllAsync(bool asNoTracking = true,
                                                Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        return await Get(asNoTracking, include).ToListAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _averyLamDbContext.Set<TEntity>().Update(entity); await _averyLamDbContext.SaveChangesAsync();
    }


    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _averyLamDbContext.Set<TEntity>().AnyAsync(predicate);
    }


    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _averyLamDbContext.Set<TEntity>().CountAsync(predicate);
    }
    private IQueryable<TEntity> Get(bool asNoTracking = true,
                                    Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
    {
        IQueryable<TEntity> entity = _averyLamDbContext.Set<TEntity>();

        if (asNoTracking)
        {
            entity = entity.AsNoTracking();
        }

        entity = include?.Invoke(entity) ?? entity; // Apply includes if provided

        return entity;
    }
}