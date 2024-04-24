using Market.DAL;
using Market.DTO;
using Market.Models;

namespace Market.Services
{
    internal interface IUserService
    {
        Task<DbResult> CreateUser(CreateUserDto user);

        Task<DbResult> SetUserSeller(Guid userId, bool isSeller);

        Task<DbResult<User>> IsUserExists(string login, string password);
    }
}
