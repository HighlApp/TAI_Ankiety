using System.Linq;
using Microsoft.AspNetCore.Http;
using Surveys.Infrastructure.Common;

namespace Surveys.Infrastructure.Extensions
{
    public static class HttpContextExtensionAccessor
    {
        public static string GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext.User?.Claims
                .Single(x => x.Type == ClaimNames.Id).Value;
        }
    }
}
