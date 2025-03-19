namespace Mango.Services.AuthAPI.Abstracts.Constants;

public static class Permissions
{
    public static string Type { get; } = "permissions";

    public static IList<string> GetAllPermissions =>
        typeof(Permissions).GetFields().Select(f => f.GetValue(f) as string).ToList()!;
}