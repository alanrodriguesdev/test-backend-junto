using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Domain.Interfaces;
using TestBackendUser.Domain.Models;
using TestBackendUser.Domain.Response;
using TestBackendUser.Service;
using TestBackendUser.Service.Interfaces;
using Xunit;

namespace TestBackendUser.XUnitTest.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;       
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockMapper = new Mock<IMapper>();

            _userService = new UserService(
                _mockUserRepository.Object,
                _mockMapper.Object);
        }

        #region Login Test
        [Fact]
        public async void Login_ShouldLogin_WhenDataIsValid()
        {
            // Arrange
            var command = new LoginCommand()
            {
                Email = "email@email.com",
                Password = "1234"
            };

            _mockUserRepository
                .Setup(x => x.VerifyByLoginAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => new Usuario() { Email = "xaxaxa", Senha = command.Password  });

            // Act
            var response = await _userService.Login(command);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Empty(response.Errors);
        }      

        [Fact]
        public async void Login_ShouldNotLogin_WhenDataIsInvalid()
        {
            // Arrange
            var command = new LoginCommand()
            {
                Email = "email@email.com",
                Password = "1234"
            };

            _mockUserRepository
                .Setup(x => x.VerifyByLoginAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => null);

            // Act
            var response = await _userService.Login(command);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async void Login_ShouldValidateCommand_WhenEmailIsInvalid()
        {
            // Arrange
            var command = new LoginCommand()
            {
                Email = "teste@",
                Password = "1234"
            };           

            // Act
            var response = await _userService.ValidaLogin(command);

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void Login_ShouldValidateCommand_WhenEmailIsvalid()
        {
            // Arrange
            var command = new LoginCommand()
            {
                Email = "teste@teste.com",
                Password = "1234"
            };

            // Act
            var response = await _userService.ValidaLogin(command);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async void Login_ShouldValidateCommand_WhenPassWordIsInvalid()
        {
            // Arrange
            var command = new LoginCommand()
            {
                Email = "teste@teste.com",
                Password = "1234"
            };

            // Act
            var response = await _userService.ValidaLogin(command);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async void Login_ShouldValidateCommand_WhenPassWordIsInInvalid()
        {
            // Arrange
            var command = new LoginCommand()
            {
                Email = "teste@teste.com",
                Password = ""
            };

            // Act
            var response = await _userService.ValidaLogin(command);

            // Assert
            Assert.NotEmpty(response);
        }
        #endregion

        #region Insert Test
        [Fact]
        public async void Create_ShouldCreate_WhenDataIsValid()
        {
            // Arrange
            var command = new UserCommand()
            {
                Name = "teste teste",
                Email = "email@email.com",
                Password = "1234"
            };

            Usuario usuario = new Usuario() { Email = command.Email, Nome = command.Name, Senha = command.Password };

            _mockUserRepository
                .Setup(x =>  x.Insert(It.IsAny<Usuario>()))
                .ReturnsAsync(() => new Usuario() { Id = 4, Email = command.Email, Nome = command.Name, Senha = command.Password });

            // Act
            var response = await _userService.Insert(command);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Empty(response.Errors);
        }

        [Fact]
        public async void Create_ShouldValidateCommand_WhenIsInvalid()
        {
            // Arrange
            var command = new UserCommand()
            {
                Name = "teste teste",
                Email = "email@email.com",
                Password = "1234"
            };

            // Act
            var response = await _userService.ValidatesUser(command);

            // Assert
            Assert.Empty(response);
        }
        [Fact]
        public async void Create_ShouldValidateCommand_WhenIsNotInvalid()
        {
            // Arrange
            var command = new UserCommand()
            {
                Name = "teste teste",
                Email = "email@",
                Password = null
            };

            // Act
            var response = await _userService.ValidatesUser(command);

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void Create_ShouldValidateCommand_WhenEmailIsNotInvalid()
        {
            // Arrange
            var command = new UserCommand()
            {
                Name = "teste teste",
                Email = "emaail",
                Password = "1234"
            };

            // Act
            var response = await _userService.ValidatesUser(command);

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void Create_ShouldValidateCommand_WhenPasswordIsNotInvalid()
        {
            // Arrange
            var command = new UserCommand()
            {
                Name = "teste teste",
                Email = "emaail",
                Password = null
            };

            // Act
            var response = await _userService.ValidatesUser(command);

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void Create_ShouldValidateCommand_WhenEmailExist()
        {
            // Arrange
            var command = new UserCommand()
            {
                Name = "teste teste",
                Email = "teste@teste.com",
                Password = "1234"
            };

            _mockUserRepository
               .Setup(x => x.ExistEmail(It.IsAny<string>()))
               .ReturnsAsync(() => true);

            // Act
            var response = await _userService.Insert(command);

            // Assert
            Assert.False(response.Success);
            Assert.NotEmpty(response.Errors);
        }
        #endregion

        #region Update Test
        [Fact]
        public async void Update_ShouldUpdate_WhenDataIsValid()
        {
            // Arrange
            var command = new UpdateUserCommand()
            {
                Name = "teste teste",
                Email = "email@email.com",
                Password = "1234"
            };
            int userId = 4;

            _mockUserRepository
            .Setup(x => x.ExistEmailUpdate(It.IsAny<string>(),It.IsAny<int>()))
            .ReturnsAsync(() => false );

            _mockUserRepository
                .Setup(x => x.Update(It.IsAny<Usuario>()))
                .ReturnsAsync(() => new Usuario() { Id = 4, Email = command.Email, Nome = command.Name, Senha = command.Password });

            // Act
            var response = await _userService.Update(command,userId);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Empty(response.Errors);
        }

        [Fact]
        public async void Update_ShouldValidateCommand_WhenIsInvalid()
        {
            // Arrange
            var command = new UpdateUserCommand()
            {
                Name = "teste teste",
                Email = "email@email.com",
                Password = "1234"
            };
            int userId = 4;

            // Act
            var response = await _userService.ValidatesUpdateUser(command, userId);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async void Update_ShouldValidateCommand_WhenIsNotInvalid()
        {
            // Arrange
            var command = new UpdateUserCommand()
            {
                Name = "teste teste",
                Email = "email@email.com",
                Password = "1234"
            };
            int userId = 0;

            // Act
            var response = await _userService.ValidatesUpdateUser(command, userId);

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void Update_ShouldValidateCommand_WhenEmailExist()
        {
            // Arrange
            var command = new UpdateUserCommand()
            {
                Name = "teste teste",
                Email = "teste@teste.com",
                Password = "1234"
            };
            int userId = 4;

            _mockUserRepository
               .Setup(x => x.ExistEmail(It.IsAny<string>()))
               .ReturnsAsync(() => true);

            // Act
            var response = await _userService.Update(command,userId);

            // Assert
            Assert.False(response.Success);
            Assert.NotEmpty(response.Errors);
        }
        #endregion

        #region Delete Test
        [Fact]
        public async void Delete_ShouldDelete_WhenUserIsValid()
        {
            // Arrange
            var command = new DeleteUserCommand()
            {
                UserId = 4
            };

            _mockUserRepository
             .Setup(x => x.SelectByUserId(It.IsAny<int>()))
             .ReturnsAsync(() => new Usuario() { Nome = "Teste Teste", Email = "teste@teste.com" });

            _mockUserRepository
               .Setup(x => x.Delete(It.IsAny<int>())).Verifiable();

            // Act
            var response = await _userService.Delete(command);

            // Assert
            Assert.True(response.Success);
            Assert.Empty(response.Errors);
        }

        [Fact]
        public async void Delete_ShouldValidateCommand_WhenIsValid()
        {
            // Arrange
            var command = new DeleteUserCommand()
            {
                UserId = 4
            };
           

            // Act
            var response = await _userService.ValidaDelete(command);

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async void Delete_ShouldValidateCommand_WhenIsInvalid()
        {
            // Arrange
            var command = new DeleteUserCommand()
            {
                UserId = 0
            };


            // Act
            var response = await _userService.ValidaDelete(command);

            // Assert
            Assert.NotEmpty(response);
        }

        [Fact]
        public async void Delete_ShouldUserNotExist_ByUserId()
        {
            // Arrange
            var command = new DeleteUserCommand()
            {
                UserId = 4
            };            

            _mockUserRepository
               .Setup(x => x.SelectByUserId(It.IsAny<int>()))
               .ReturnsAsync(() => null);

            // Act
            var response = await _userService.Delete(command);

            // Assert
            Assert.False(response.Success);
            Assert.NotEmpty(response.Errors);
        }

        [Fact]
        public async void Delete_ShouldUserExist_ByUserId()
        {
            // Arrange
            var command = new DeleteUserCommand()
            {
                UserId = 4
            };

            _mockUserRepository
               .Setup(x => x.SelectByUserId(It.IsAny<int>()))
               .ReturnsAsync(() => new Usuario() { Nome = "Teste Teste",Email = "teste@teste.com"});

            // Act
            var response = await _userService.Delete(command);

            // Assert
            Assert.True(response.Success);
            Assert.Empty(response.Errors);
        }
        #endregion
    }
}
