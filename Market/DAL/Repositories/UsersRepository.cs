
using Market.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL.Repositories
{
    internal sealed class UsersRepository
    {
        private readonly RepositoryContext _context;

        public UsersRepository()
        {
            _context = new RepositoryContext();
        }

        internal async Task<DbResult> CreateUserAsync(User user)
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

        internal async Task<DbResult> SetUserIsSeller(Guid customerId, bool isSellers)
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

        //internal async Task<DbResult<User>> GetUserByLoginPassword()
        //{

        //}
    }
}
