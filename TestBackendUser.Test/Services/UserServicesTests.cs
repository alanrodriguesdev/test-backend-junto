using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestBackendUser.Domain.Interfaces;
using TestBackendUser.Service;

namespace TestBackendUser.Test.Services
{
    public class UserServicesTests
    {
        public class UserServiceTests
        {
            private readonly Mock<IUserRepository> _mockUserRepository;
            private readonly Mock<IMapper> _mockMapper;
            private readonly UserService _userService;

            public UserServiceTests()
            {
                _mockUserRepository = new Mock<IUserRepository>();
                _mockMapper = new Mock<IMapper>();

                _userService = new UserService(
                    _mockUserRepository.Object,
                    _mockMapper.Object);
            }

            //[Fact]
            //public async void Login_ShouldLogin_WhenDataIsValid()
            //{
            //    // Arrange
            //    var command = new LoginCommand()
            //    {
            //        Email = "email@email.com",
            //        Password = "1234"
            //    };

            //    _mockUserRepository
            //        .Setup(x => x.VerifyByLoginAndPassword(It.IsAny<string>(), It.IsAny<string>()))
            //        .ReturnsAsync(() => new Usuario() { Email = command.Email });

            //    // Act
            //    var response = await _userService.Login(command);

            //    // Assert
            //    Assert.NotNull(response);
            //    Assert.True(response.Success);
            //    Assert.Empty(response.Errors);
            //}

            //[Fact]
            //public async void Login_ShouldNotLogin_WhenDataIsInvalid()
            //{
            //    // Arrange
            //    var command = new LoginCommand()
            //    {
            //        Email = "email@email.com",
            //        Password = "1234"
            //    };

            //    _mockUserRepository
            //        .Setup(x => x.VerifyByLoginAndPassword(It.IsAny<string>(), It.IsAny<string>()))
            //        .ReturnsAsync(() => null);

            //    // Act
            //    var response = await _userService.Login(command);

            //    // Assert
            //    Assert.False(response.Success);
            //}
        }
    }
}
