using System.Linq.Expressions;

namespace Mango.Services.CouponAPI.Repositories;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void Delete(T entity);
    Task DeleteByIdAsync(CancellationToken ct = default, params object[] keyValues);
    Task<T> FindByIdAsync(CancellationToken ct = default, params object[] keyValues);
    Task<T> Get(Expression<Func<T, bool>> predicate, string? includes = null, bool tracked = false, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, string? includes = null, bool tracked = false, CancellationToken ct = default);
}
