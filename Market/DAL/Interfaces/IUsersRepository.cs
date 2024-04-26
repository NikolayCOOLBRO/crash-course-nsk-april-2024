using Market.Models;

namespace Market.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<DbResult> CreateUserAsync(User user);
        Task<DbResult> SetUserIsSeller(Guid customerId, bool isSellers);
        Task<DbResult<User>> GetUserByLoginPassword(string logIn, string password);
    }
}
