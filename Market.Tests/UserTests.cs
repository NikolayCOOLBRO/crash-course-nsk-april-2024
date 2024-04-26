using FluentValidation;
using FluentValidation.Results;
using Market.Controllers;
using Market.DAL.Interfaces;
using Market.DTO;
using Market.Services;
using Market.UseCases.Validators.Users;
using NSubstitute;

namespace Market.Tests
{
    public class UserTests
    {
        private IValidator<CreateUserDto> _validator;
        private IValidator<CreateUserDto> _fakeValidator;
        private CreateUserDto _user;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _validator = new CreateUserValidator();
            _fakeValidator = Substitute.For<IValidator<CreateUserDto>>();

            _fakeValidator.Validate(null!).ReturnsForAnyArgs(new ValidationResult());
        }

        [SetUp]
        public void Setup()
        {
            _user = DataUserTestGenerator.GetValidUser();
        }

        [Test]
        public void EmptyUserShouldFail()
        {
            var user = new CreateUserDto();

            var validationResult = _validator.Validate(user);

            Assert.IsFalse(validationResult.IsValid);
        }

        [Test]
        public void SomeValidUserShouldPass()
        {
            var validationResult = _validator.Validate(_user);

            Assert.IsTrue(validationResult.IsValid);
        }

        [Test]
        public void SomeEmptyUserNameShouldFail()
        {
            _user.Name = string.Empty;

            var validationResult = _validator.Validate(_user);

            Assert.IsFalse(validationResult.IsValid);
        }

        [Test]
        public void SomeEmptyUserLoginShouldFail()
        {
            _user.Login = string.Empty;

            var validationResult = _validator.Validate(_user);

            Assert.IsFalse(validationResult.IsValid);
        }

        [Test]
        public void SomeEmptyUserPasswordShouldFail()
        {
            _user.Password = string.Empty;

            var validationResult = _validator.Validate(_user);

            Assert.IsFalse(validationResult.IsValid);
        }

        [Test]
        public void SomeInvalidPasswordUserShouldFail()
        {
            _user.Password = DataUserTestGenerator.GetInvalidPassword();

            var validationResult = _validator.Validate(_user);

            Assert.IsFalse(validationResult.IsValid);
        }

        /*
        [Test]
        public async Task Test2()
        {
            var userServices = Substitute.For<IUserService>();

            var userValidator = Substitute.For<IValidator<CreateUserDto>>();
            var userController = new UsersController(userServices, userValidator);

            await userController.CreateUser(new CreateUserDto());
        }
        */
    }
}