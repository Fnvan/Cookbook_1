using System.Security.Claims;

namespace Cookbook_1.Extentions
{
    public static class HttpContextExtension
    {
        public static int? ExtractUserIdFromClaims(this HttpContext context)
        {
            var claim = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return claim is null ? null : int.Parse(claim.Value);
        }
    }
}
