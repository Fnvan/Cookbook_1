using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cookbook_1.Politics
{
    public class PostOwnerRequirementHandler(IHttpContextAccessor accessor) : AuthorizationHandler<PostOwnerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostOwnerRequirement requirement)
        {
            //ищем в контексте у пользователя клейм с идентификатором
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            //Получаем httpContext запроса, чтобы достать из строки запроса userId
            var accessorResult = accessor
                .HttpContext!
                .Request
                .Query
                .TryGetValue("userid", out var userIdQuery);
            if(!accessorResult || !userIdQuery.Any())
            {
                context.Fail();
                return Task.CompletedTask;
            }
            //Проверка, что заголовок содержит того же пользователя, что и строка запроса
            if (userIdClaim.Value != userIdQuery.First())
            {
                context.Fail();
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
