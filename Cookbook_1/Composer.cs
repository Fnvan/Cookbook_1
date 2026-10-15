using Cookbook_1.Abstractions;
using Cookbook_1.Database;
using Cookbook_1.Politics;
using Cookbook_1.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text.Json.Serialization;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;


namespace Cookbook_1
{
    public static class Composer
    {
        //Тут добавляются инфраструктурные сервисы
        //которые нужны для передачи данных внутри приложения
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(Program));

            //Подключение БД
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                //Указываю ссылку на строку подключения из secrets.json, чтобы пароль не передать
                options.UseNpgsql(
                    configuration.GetConnectionString("ConnectionString")
                    );
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwtOptions = configuration.GetRequiredSection(nameof(JwtOptions))
                    .Get<JwtOptions>()!; //"!" чтобы указать, что тут 100% не null
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        //Проверять ли того, кто выпустил токен
                        ValidateIssuer = true,
                        // Того, кто получил токен
                        ValidateAudience = true,
                        //Время жизни токена
                        ValidateLifetime = true,
                        //Секретный ключ
                        ValidateIssuerSigningKey = true,
                        //Сервис, который отвечает за выдачу токена
                        ValidIssuer = jwtOptions.Issuer,
                        //Сервис, которому можно выдать токен
                        ValidAudience = jwtOptions.Subject,
                        //Секрет, который используется для шифрования
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtOptions.Secrets))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        //Валидация токена
                        OnTokenValidated = context =>
                        {
                            var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                            //Достаём id пользователя
                            var userIdString = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
                            
                            if (!int.TryParse(userIdString, out var userId))
                            {
                                context.Fail("Unauthorized");
                                return Task.CompletedTask;
                            }

                            if(context.SecurityToken.ValidTo < DateTime.UtcNow
                            || !authService.VerifyToken(userId, context.SecurityToken.UnsafeToString()))
                            {
                                context.Fail("Unauthorized");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder =
                    new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

                //Политика проверки пользователя из токена
                options.AddPolicy("PostOwner", policy =>
                {
                    //Отсекаем не аутентифицированных
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new PostOwnerRequirement());
                });
            });
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                    });

                options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddExceptionHandler<ExceptionHandler>();
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetRequiredSection(nameof(JwtOptions)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddValidatorsFromAssembly(typeof(Composer).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddControllers().AddJsonOptions(options =>
            {
                // Говорим сериализатору обрабатывать циклы
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            }); ;
            return services;
            
        }
    }
}
