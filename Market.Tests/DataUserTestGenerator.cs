using Market.DTO;

namespace Market.Tests
{
    public class DataUserTestGenerator
    {
        public static CreateUserDto GetValidUser()
        {
            return new CreateUserDto()
            {
                Name = GetValidUserName(),
                Login = GetValidLogin(),
                Password = GetValidPassword(),
            };
        }

        public static string GetValidPassword()
        {
            return "TestTest";
        }

        public static string GetValidUserName()
        {
            return "Test";
        }

        public static string GetValidLogin()
        {
            return "Test";
        }

        public static string GetInvalidPassword()
        {
            return "Test";
        }
    }
}