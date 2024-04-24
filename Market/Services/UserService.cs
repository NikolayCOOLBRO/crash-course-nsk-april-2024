using Market.DAL;
using Market.DAL.Repositories;
using Market.DTO;
using Market.Misc;
using Market.Models;

namespace Market.Services
{
    internal class UserService : IUserService
    {
        private readonly UsersRepository _usersRepository;

        public UserService()
        {
            _usersRepository = new UsersRepository();
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

        public async Task<DbResult<bool>> IsUserExists(string login, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<DbResult> SetUserSeller(Guid userId, bool isSeller)
        {
            return await _usersRepository.SetUserIsSeller(userId, isSeller);
        }
    }
}
