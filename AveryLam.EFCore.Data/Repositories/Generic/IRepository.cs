using System.Linq.Expressions;

namespace AveryLam.EFCore.Data.Repositories.Generic;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task AddManyAsync(IEnumerable<TEntity> entities);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    Task DeleteAsync(TEntity entity);
    Task DeleteManyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
    Task<List<TEntity>> GetAllAsync(bool asNoTracking = true, Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null);
    Task UpdateAsync(TEntity entity);
}