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
            CreateMap<Ingredient, Ingredient>();
                
        }
    }
}
