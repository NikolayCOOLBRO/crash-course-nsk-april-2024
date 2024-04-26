using Market.DAL;
using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Misc;
using Market.Models;

namespace Market.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;

        public UserService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<DbResult> CreateUser(CreateUserDto dto)
        {
            var salt = Guid.NewGuid().ToString();
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LogIn = dto.Login,
                Password = PasswordHasher.GeneratePaswordHash(dto.Password + salt),
                Salt = salt,
                IsSeller = false
            };

            var result = await _usersRepository.CreateUserAsync(user);
            return result;
        }

        public async Task<DbResult<User>> IsUserExists(string login, string password)
        {
            var result = await _usersRepository.GetUserByLoginPassword(login, password);

            return result;
        }

        public async Task<DbResult> SetUserSeller(Guid userId, bool isSeller)
        {
            return await _usersRepository.SetUserIsSeller(userId, isSeller);
        }
    }
}
