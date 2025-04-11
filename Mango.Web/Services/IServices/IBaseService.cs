using Mango.Web.Abstracts;

namespace Mango.Web.Services.IServices;

public interface IBaseService
{
    Task<Result<T>> SendAsync<T>(Request request, CancellationToken ct = default);
    Task<Result> SendAsync(Request request, CancellationToken ct = default);
}
