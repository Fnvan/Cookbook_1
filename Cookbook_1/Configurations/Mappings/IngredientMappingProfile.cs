using AutoMapper;
using Cookbook_1.Contracts;
using Cookbook_1.Models;

namespace Cookbook_1.Configurations.Mappings
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            //Указываем, из какого типа хотим получить целевой тип;
            // Связанные сущности заполняются сервисом или EF, а не из DTO.
            CreateMap<AddIngredientToRecipeDto, IngredientInRecipe>()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.Ingredient, opt => opt.Ignore());
                
        }
    }
}
