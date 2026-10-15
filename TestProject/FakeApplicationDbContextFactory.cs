using Cookbook_1.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Cookbook_1.Models;

namespace TestProject
{
    public class FakeApplicationDbContextFactory
    {
        //Seed-данные для тестов.
        public const int UserAId = 1;

        public const int UserBId = 2;

        public const int RecipeIdToRead = 1;
        public const int RecipeIdToUpdate = 1;
        public const int RecipeIdToDelete = 1;
        public const int IngredientId = 1;
        
        public static (ApplicationDbContext context, SqliteConnection connection) Create()
        {
            var connection = new SqliteConnection("DataSource =:memory:");
            //открытие подключения
            connection.Open();

            //Создаем DbContextOptionsBuilder для ApplicationDbContext с подключением
            //По сути говорим, на что ориентироваться и где брать данные для ApplicationDbContext.
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            var environment = new FakeWebHostEnvironment();

            //Создаем объект контекста с нашими опциями
            var context = new ApplicationDbContext(options, environment);

            context.Database.EnsureCreated();

            //Добавляем тестовые данные стандартным способом
            context.Users.AddRange(
                new Cookbook_1.Models.User
                {
                    Id = UserAId,
                    Login = "A",
                    Password = "123",
                },
                new Cookbook_1.Models.User
                {
                    Id = UserBId,
                    Login = "A",
                    Password = "123",
                });

            context.Ingredients.AddRange(
                new Cookbook_1.Models.Ingredient
                {
                    Id = IngredientId,
                    Name = "A",
                });

            context.Recipes.AddRange(
                new Cookbook_1.Models.Recipe
                {
                    Id = RecipeIdToRead,
                    Name = "A",
                    UserId = UserAId,
                });

            context.SaveChanges();
            //Важно отчистить трекинг, чтобы EF начал отслеживать данные выше, как уже добавленные в бд
            context.ChangeTracker.Clear();

            return (context, connection);
        }

        //Добавляем метод для отчитки после тестов
        public static void Destroy(ApplicationDbContext context, SqliteConnection connection)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
            connection.Dispose();
        }


        public class FakeWebHostEnvironment : IWebHostEnvironment
        {
            public string EnvironmentName { get; set; } = Environments.Development;
            public string ApplicationName { get; set; } = "TestProject";
            public string WebRootPath { get; set; } = string.Empty;
            public IFileProvider WebRootFileProvider { get; set; } = new NullFileProvider();
            public string ContentRootPath { get; set; } = string.Empty;
            public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
        }
    }
}
