using Cookbook_1.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Cookbook_1.Services
{
    public class ExceptionHandler : IExceptionHandler 
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) //ЧТо за ValueTask ещё
        {
            httpContext.Response.StatusCode = exception switch 
            {
                RecipeNotFoundException => (int)HttpStatusCode.NotFound,
                RecipeAlreadyExistsException => (int)HttpStatusCode.Conflict,
                IngredientNotFoundException => (int)HttpStatusCode.NotFound,
                IngredientAlreadyExistsException => (int)HttpStatusCode.Conflict,
            };

            await httpContext.Response.WriteAsync(exception.Message); 
            return true;
            
        }
    }
}
