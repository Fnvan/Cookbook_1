using AutoMapper;
using Cookbook_1.Abstractions;
using Cookbook_1.Contracts;
using Cookbook_1.Exceptions;
using Cookbook_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Cookbook_1.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;

        public UserService(IMapper mapper, IApplicationDbContext applicationDbContext)
        {
            _mapper = mapper; 
            _applicationDbContext = applicationDbContext;
        }
        public async Task<UserVm> CreateNewUserAsync(CreateUserDto user)
        {
            var newUser =  _mapper.Map<User>(user);
            await _applicationDbContext.Users.AddAsync(newUser);
            _applicationDbContext.SaveChanges();
            var userVm = _mapper.Map<UserVm>(newUser);
            return userVm;
        }

        public async Task<UserVm> GetUserProfileAsync(int userId)
        {
            var user = await GetUserWithIngredientsOrThrowException(userId);
            var userVm = _mapper.Map<UserVm>(user);
            return userVm;
        }

        public async Task <UserVm> UpdateUserAsync(UpdateUserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserPasswordAsync(UpdateUserPasswordDto userPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Recipe>> ShowUserRecipesAsync(int userId)
        {
            throw new NotImplementedException();
        }

        private async Task<User> GetUserWithIngredientsOrThrowException(int userId)
        {
            var user = await _applicationDbContext.Users.Where(user => user.Id == userId)
                .Include(r => r.UserRecipes).FirstOrDefaultAsync() ?? throw new UserNotFoundException(userId);
            return user;
        }
    }
}
