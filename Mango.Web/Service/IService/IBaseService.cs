using Mango.Web.Abstracts;

namespace Mango.Web.Service.IService;

public interface IBaseService
{
    Task<Result<T>> SendAsync<T>(Request request);
    Task<Result> SendAsync(Request request);
}
