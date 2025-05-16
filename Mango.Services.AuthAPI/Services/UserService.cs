using Store.Services.AuthAPI.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Store.Services.AuthAPI.Contracts.Users;
using Store.Abstractions.Abstraction;
using Store.Services.AuthAPI.Errors;
using Carter.Response;

namespace Store.Services.AuthAPI.Services;

public class UserService(AppDbContext _context, IFileService _fileService) : IUserService
{

    public async Task<Result> UpdateProfileAsync(string userId, UpdateUserRequest request, CancellationToken ct = default)
    {
        await _context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(
                u => u.SetProperty(x => x.FirstName, request.FirstName)
                      .SetProperty(x => x.LastName, request.LastName),
                ct);

        return Result.Success();
    }

    public async Task<Result> UpdateProfileImageAsync(string userId, IFormFile image, CancellationToken ct = default)
    {
        var user = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => new { u.ImageUrl })
            .FirstOrDefaultAsync(ct);

        if (user is null)
            return UserErrors.UserNotFound;

        var newImage = await _fileService.UpdateImageAsync(image, user.ImageUrl!, ct);

        if (newImage is not null)
        {
            await _context.Users
                .Where(u => u.Id == userId)
                .ExecuteUpdateAsync(
                    u => u.SetProperty(x => x.ImageUrl, newImage),
                    ct);
        }

        return Result.Success();
    }
    public async Task<Result> DeleteAccountAsync(string userId, CancellationToken ct = default)
    {
        await _context.Users
            .Where(u => u.Id == userId)
            .ExecuteDeleteAsync(ct);

        return Result.Success();
    }
    public async Task<Result<UserResponse>> GetProfileAsync(string userId, CancellationToken ct = default)
    {
        var response = await _context.Users
            .Where(u => u.Id == userId)
            .ProjectToType<UserResponse>()
            .FirstOrDefaultAsync(ct);

        if (response is null)
            return UserErrors.UserNotFound;

        return response;
    }
    

}
