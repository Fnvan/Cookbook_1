using AutoMapper;
using Cookbook_1.Contracts;
using Cookbook_1.Models;
namespace Cookbook_1.Configurations.Mappings
{
    public class RecipeMappingProfile : Profile
    {
        public RecipeMappingProfile()
        {
            //Указываем, из какого типа хотим получить целевой тип;
            //Сначала маппим вложенные модели
            CreateMap<IngredientInRecipe, IngredientInRecipeVm>()
                .ForCtorParam(nameof(IngredientInRecipeVm.IngredientName),
                 opt => opt.MapFrom(src => src.IngredientName))
                .ForCtorParam(nameof(IngredientInRecipeVm.Amount), opt => opt.MapFrom(src => src.Amount))
                .ForCtorParam(nameof(IngredientInRecipeVm.Units), opt => opt.MapFrom(src => src.Units));
            //потом сам рецепт
            CreateMap<Recipe, RecipeVm>()
                .ForCtorParam(nameof(RecipeVm.Name), opt => opt.MapFrom(src => src.Name))
                .ForCtorParam(nameof(RecipeVm.CookingDescription), opt => opt.MapFrom(src => src.CookingDescription))
                .ForCtorParam(nameof(RecipeVm.RequiredIngredients), opt => opt.MapFrom(src => src.RequiredIngredients))
                .ForCtorParam(nameof(RecipeVm.Rating), opt => opt.MapFrom(src => src.Rating));


            CreateMap<IngredientInRecipeDto, IngredientInRecipe>();
            

            //CreateMap<IngredientInRecipe, Ingredient>()
            //    .ForCtorParam(nameof(Ingredient.Id), opt => opt.MapFrom(src => src.IngredientId))
            //    .ForCtorParam(nameof(Ingredient.Name), opt => opt.MapFrom(src => src.IngredientName));
            CreateMap<CreateRecipeDto, Recipe>();

        }
    }
}
