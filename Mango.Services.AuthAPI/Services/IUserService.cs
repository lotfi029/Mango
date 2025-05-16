using Store.Abstractions.Abstraction;
using Store.Services.AuthAPI.Contracts.Users;

namespace Store.Services.AuthAPI.Services;

public interface IUserService
{
    Task<Result> UpdateProfileAsync(string userId, UpdateUserRequest request, CancellationToken ct = default);
    Task<Result> UpdateProfileImageAsync(string userId, IFormFile image, CancellationToken ct = default);
    Task<Result<UserResponse>> GetProfileAsync(string userId, CancellationToken ct = default);
    Task<Result> DeleteAccountAsync(string userId, CancellationToken ct = default);
}
