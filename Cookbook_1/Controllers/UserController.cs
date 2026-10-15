using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cookbook_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController(IAuthService authService, IUserService userService, IRecipeService recipeService) : ControllerBase
    {

        //Этот метод доступен без авторизации
        [AllowAnonymous]
        [HttpPost("/registration")]
        public async Task<ActionResult<LogInResponse>> CreateNewUser([FromBody] CreateUserDto userDto)
        {
            var token = await authService.SignUpAsync(userDto);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LogInResponse>> LogIn([FromBody] LogInUserDto dto)
        {
            var result = await authService.LogInAsync(dto.Login, dto.Password);
            if (result is null)
                return Unauthorized();

            return Ok(result); 
        }

        [HttpPost("logout")]
        public async Task<ActionResult<bool>> LogOut([FromBody] int userId)
        {
            //var userId = HttpContext.ExtractUserIdFromClaims();
            var result = await authService.LogOutAsync(userId);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task <ActionResult<LogInResponse>> Refresh([FromBody] string refreshToken)
        {
            var result = await authService.RefreshTokenAsync(refreshToken);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
    }
}
