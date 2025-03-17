using Mango.Web.Abstracts;

namespace Mango.Web.Service.IService;

public interface IBaseService<T>
{
    Task<Result<T>> SendAsync(Request request);
}
