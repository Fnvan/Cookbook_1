using Microsoft.AspNetCore.Authorization;

namespace Cookbook_1.Politics
{
    //Необходимый класс, чтобы создать кастомное условие автоизации
    public class PostOwnerRequirement :IAuthorizationRequirement;

}
