using Mango.Services.CouponAPI.Persistence;

namespace Mango.Services.CouponAPI.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context 
        = context ?? throw new ArgumentNullException(nameof(context));

    public IRepository<T> GetRepository<T>() where T : class
        => new Repository<T>(_context);
    

    public ICouponRepository Coupon 
        => new CouponRepository(_context);

    private bool _disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task CommitChangesAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
