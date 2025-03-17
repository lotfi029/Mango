namespace Mango.Services.CouponAPI.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : class;
    ICouponRepository Coupon { get; }
    Task CommitChangesAsync(CancellationToken ct = default);
}