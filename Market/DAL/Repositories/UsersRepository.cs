
using Market.DAL.Interfaces;
using Market.Misc;
using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories
{
    internal sealed class UsersRepository : IUsersRepository
    {
        private readonly IRepositoryContext _context;

        public UsersRepository(IRepositoryContext context)
        {
            _context = context;
        }

        public async Task<DbResult> CreateUserAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return DbResult.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult> SetUserIsSeller(Guid customerId, bool isSellers)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id.Equals(customerId));

            if (user == null)
            {
                return DbResult.NotFound();
            }

            try
            {
                user.IsSeller = isSellers;
                await _context.SaveChangesAsync();
                return DbResult.Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return DbResult.Of(DbResultStatus.UnknownError);
            }
        }

        public async Task<DbResult<User>> GetUserByLoginPassword(string logIn, string password)
        {
            var userByLogIn = await _context.Users.FirstOrDefaultAsync(item => item.LogIn.Equals(logIn));

            if (userByLogIn == null)
            {
                return DbResult<User>.NotFound();
            }

            var hash = PasswordHasher.GeneratePaswordHash(password + userByLogIn.Salt);

            if (!hash.Equals(userByLogIn.Password))
            {
                return DbResult<User>.NotFound();
            }

            return DbResult<User>.Ok(userByLogIn);
        }
    }
}
