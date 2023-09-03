using System.Security.Claims;

namespace Infrastructure.Auth.Microsoft.Extensions;

internal static class ClaimsPrincipalExtensions
{
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        List<string>? result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal)
    {
        List<string>? result = claimsPrincipal?.FindAll("claims")?.Select(x => x.Value).ToList();
        
        if (result is not null && result.Any())
        {
            var value = result.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(value))
                result = new List<string>(value.Split(";"));
        }

        return result;
    }
}