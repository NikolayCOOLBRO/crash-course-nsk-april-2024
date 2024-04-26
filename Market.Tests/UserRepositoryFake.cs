using Market.DAL;
using Market.DAL.Interfaces;
using Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Tests
{
    public class UserRepositoryFake : IUsersRepository
    {
        private int _counterCreateUser = 0;
        private int _counterGetUser = 0;
        private int _counterSetUser = 0;

        public int CountCreateUser => _counterCreateUser;
        public int CountGetUser => _counterGetUser;
        public int CounterSetUser => _counterSetUser;

        public async Task<DbResult> CreateUserAsync(User user)
        {
            _counterCreateUser++;
            return DbResult.Ok();
        }

        public async Task<DbResult<User>> GetUserByLoginPassword(string logIn, string password)
        {
            _counterGetUser++;
            return DbResult<User>.Ok(null!);
        }

        public async Task<DbResult> SetUserIsSeller(Guid customerId, bool isSellers)
        {
            _counterSetUser++;
            return DbResult.Ok();
        }
    }
}
