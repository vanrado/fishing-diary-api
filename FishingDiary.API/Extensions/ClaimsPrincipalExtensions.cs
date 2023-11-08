using System.Security.Claims;

namespace FishingDiary.API;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirst("userId")?.Value;
    }
}
