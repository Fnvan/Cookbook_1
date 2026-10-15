using Cookbook_1.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cookbook_1
{
    public static class Composer
    {
        //Тут добавляются инфраструктурные сервисы
        //которые нужны для передачи данных внутри приложения
        //public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        //{
        //    // Добавляем автомаппер и регистрируем в нем все классы из
        //    // нашего проекта, которые мы наследовали от Profile
        //    services.AddAutoMapper(typeof(Composer).Assembly);
        //    services.AddExceptionHandler<ExceptionHandler>();
        //    services.AddControllers();
        //    return services;
        //}
    }
}
