using AutoMapper;
using Cookbook_1.Configurations.Mappings;
using Cookbook_1.Contracts;
using Cookbook_1.ENums;
using Cookbook_1.Models;

namespace TestProject
{
    public class AutoMapperProfileTests
    {
        private readonly MapperConfiguration _configuration;
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            // Как в Composer: загружаем все профили из сборки приложения.
            _configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(typeof(RecipeMappingProfile).Assembly));
            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ProfilesConfiguration_IsValid()
        {
            // При ошибке метод выбросит исключение, и xUnit отметит тест как упавший.
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void AddIngredientToRecipeDto_MapsIdsAmountAndUnits()
        {
            // Arrange — подготавливаем входные данные.
            var dto = new AddIngredientToRecipeDto(10, 20, 125.5, Units.Gram);

            // Act — выполняем проверяемое действие.
            var result = _mapper.Map<IngredientInRecipe>(dto);

            // Assert — проверяем результат.
            Assert.Equal(10, result.RecipeId);
            Assert.Equal(20, result.IngredientId);
            Assert.Equal(125.5, result.Amount);
            Assert.Equal(Units.Gram, result.Units);
        }

        [Fact]
        public void AddNewIngredientToRecipeDto_MapsIdsAmountAndUnits()
        {
            var dto = new AddNewIngredientToRecipeDto(10, 20, "Мука", 0.5, Units.Kilogram);

            var result = _mapper.Map<IngredientInRecipe>(dto);

            Assert.Equal(10, result.RecipeId);
            Assert.Equal(20, result.IngredientId);
            Assert.Equal(0.5, result.Amount);
            Assert.Equal(Units.Kilogram, result.Units);
        }

        [Theory]
        [InlineData(Units.Gram, "Gram")]
        [InlineData(Units.Kilogram, "Kilogram")]
        [InlineData(Units.BigSpoon, "BigSpoon")]
        [InlineData(Units.SmallSpoon, "SmallSpoon")]
        [InlineData(Units.Pieces, "Pieces")]
        [InlineData(Units.Mugs, "Mugs")]
        public void IngredientInRecipe_MapsIngredientNameAndUnitsText(Units units, string expectedUnits)
        {
            var ingredient = new IngredientInRecipe
            {
                Ingredient = new Ingredient { Id = 20, Name = "Мука" },
                IngredientId = 20,
                Amount = 125.5,
                Units = units
            };

            var result = _mapper.Map<IngredientInRecipeVm>(ingredient);

            Assert.Equal("Мука", result.IngredientName);
            Assert.Equal(125.5, result.Amount);
            Assert.Equal(expectedUnits, result.Units);
        }

        [Fact]
        public void CreateRecipeDto_MapsRecipeWithNestedIngredients()
        {
            var dto = new CreateRecipeDto("Блины", "Смешать и испечь",
                [new IngredientInRecipeDto(20, 125.5, Units.Gram),
                 new IngredientInRecipeDto(30, 2, Units.Pieces)]);

            var result = _mapper.Map<Recipe>(dto);

            Assert.Equal("Блины", result.Name);
            Assert.Equal("Смешать и испечь", result.CookingDescription);
            Assert.Collection(result.RequiredIngredients,
                first =>
                {
                    Assert.Equal(20, first.IngredientId);
                    Assert.Equal(125.5, first.Amount);
                    Assert.Equal(Units.Gram, first.Units);
                },
                second =>
                {
                    Assert.Equal(30, second.IngredientId);
                    Assert.Equal(2d, second.Amount);
                    Assert.Equal(Units.Pieces, second.Units);
                });
        }

        [Fact]
        public void Recipe_MapsViewModelWithNestedIngredients()
        {
            var recipe = CreateRecipe();

            var result = _mapper.Map<RecipeVm>(recipe);

            Assert.Equal(7, result.UserId);
            Assert.Equal("Блины", result.Name);
            Assert.Equal("Смешать и испечь", result.CookingDescription);
            Assert.Equal(4.5, result.Rating);
            var ingredient = Assert.Single(result.RequiredIngredients);
            Assert.Equal("Мука", ingredient.IngredientName);
            Assert.Equal(125.5, ingredient.Amount);
            Assert.Equal("Gram", ingredient.Units);
        }

        [Fact]
        public void CreateUserDto_MapsLoginAndPassword()
        {
            var dto = new CreateUserDto("cook", "test-password");

            var result = _mapper.Map<User>(dto);

            Assert.Equal("cook", result.Login);

            Assert.Equal("test-password", result.Password);
        }

        [Fact]
        public void UpdateUserDto_ChangesLoginAndPreservesOtherFields()
        {
            var recipe = CreateRecipe();
            var rating = new Rating { RatedUserId = 7, RatedRecipeId = 10, Value = RecipeRating.Good };
            var user = new User
            {
                Id = 7,
                Login = "old-login",
                Password = "existing-hash",
                UserRecipes = [recipe],
                UserRates = [rating]
            };

            // Обновляем существующий объект, а не создаём нового пользователя.
            _mapper.Map(new UpdateUserDto("new-login"), user);

            Assert.Equal("new-login", user.Login);
            Assert.Equal(7, user.Id);
            Assert.Equal("existing-hash", user.Password);
            Assert.Same(recipe, Assert.Single(user.UserRecipes));
            Assert.Same(rating, Assert.Single(user.UserRates));
        }

        [Fact]
        public void User_MapsViewModelWithRecipesAndIngredients()
        {
            var user = new User
            {
                Login = "cook",
                Password = "existing-hash",
                UserRecipes = [CreateRecipe()]
            };

            var result = _mapper.Map<UserVm>(user);

            Assert.Equal("cook", result.Login);
            var recipe = Assert.Single(result.UserRecipes);
            Assert.Equal("Блины", recipe.Name);
            Assert.Equal(7, recipe.UserId);
            Assert.Equal(4.5, recipe.Rating);
            Assert.Equal("Мука", Assert.Single(recipe.RequiredIngredients).IngredientName);
        }

        private static Recipe CreateRecipe() => new()
        {
            Id = 10,
            UserId = 7,
            Name = "Блины",
            CookingDescription = "Смешать и испечь",
            Rating = 4.5,
            RequiredIngredients =
            [
                new IngredientInRecipe
                {
                    IngredientId = 20,
                    Ingredient = new Ingredient { Id = 20, Name = "Мука" },
                    Amount = 125.5,
                    Units = Units.Gram
                }
            ]
        };
    }
}
