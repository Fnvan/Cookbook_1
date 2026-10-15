using AutoMapper;
using Cookbook_1.Contracts;
using Cookbook_1.Models;

namespace Cookbook_1.Configurations.Mappings
{
    public class UserMappingProfile :Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserRecipes, opt => opt.Ignore())
                .ForMember(dest => dest.UserRates, opt => opt.Ignore());

            // Обновление логина должно сохранять пароль и связи пользователя.
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.UserRecipes, opt => opt.Ignore())
                .ForMember(dest => dest.UserRates, opt => opt.Ignore());

            CreateMap<User, UserVm>();


        }
    }
}
