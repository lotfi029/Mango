using Microsoft.AspNetCore.Http.Metadata;

namespace Store.Services.AuthAPI.Contracts.Files;

public record ImageRequest(IFormFile Image);
