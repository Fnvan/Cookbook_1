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
                 opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForCtorParam(nameof(IngredientInRecipeVm.Amount), opt => opt.MapFrom(src => src.Amount))
                .ForCtorParam(nameof(IngredientInRecipeVm.Units), opt => opt.MapFrom(src => src.Units.ToString()));

            //потом сам рецепт
            CreateMap<Recipe, RecipeVm>()
                .ForCtorParam(nameof(RecipeVm.Name), opt => opt.MapFrom(src => src.Name))
                .ForCtorParam(nameof(RecipeVm.CookingDescription), opt => opt.MapFrom(src => src.CookingDescription))
                .ForCtorParam(nameof(RecipeVm.RequiredIngredients), opt => opt.MapFrom(src => src.RequiredIngredients))
                .ForCtorParam(nameof(RecipeVm.Rating), opt => opt.MapFrom(src => src.Rating));


            CreateMap<IngredientInRecipeDto, IngredientInRecipe>()
                .ForMember(dest => dest.RecipeId, opt => opt.Ignore())
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredient, opt => opt.Ignore());
            

            // ID, автор и оценки не поступают из запроса на создание рецепта.
            CreateMap<CreateRecipeDto, Recipe>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Rating, opt => opt.Ignore())
                .ForMember(dest => dest.ListOfRatings, opt => opt.Ignore());

            CreateMap<AddNewIngredientToRecipeDto, IngredientInRecipe>()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredient, opt => opt.Ignore());
        }
    }
}
