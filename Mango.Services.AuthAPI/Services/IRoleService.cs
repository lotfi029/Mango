using Mango.Services.AuthAPI.Abstracts;
using Mango.Services.AuthAPI.Contracts;

namespace Mango.Services.AuthAPI.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllAsync(bool? includeDisable = false, CancellationToken cancellationToken = default);
    Task<Result<RoleClaimsResponse>> GetAsync(string id);
    Task<Result<RoleClaimsResponse>> AddAsync(RoleRequest request);
    Task<Result> UpdateAsync(string id, RoleRequest request, CancellationToken cancellationToken = default);
    Task<Result> ToggleAsync(string id);
}
