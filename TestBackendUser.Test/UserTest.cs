using AutoMapper;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using TestBackendUser.CrossCutting;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Domain.Models;
using TestBackendUser.Infra.Repository;
using TestBackendUser.Service;

namespace TestBackendUser.Test
{
    public class UserTest
    {
        //public IConfiguration Configuration { get; set; }
        //public IMapper _mapper { get; set; }        
        


        //[SetUp]
        //public void Setup()
        //{
        //    var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        //    Configuration = builder.Build();
        //    string filename = @"TestBackendUser.DataBase.db";
        //    string filePath = AppDomain.CurrentDomain.BaseDirectory + filename;
            
        //    ConnectionStrings.UserConnectionString = Configuration.GetConnectionString("UserConnectionString").Replace("@path", $"{filePath}");
        //}

        //[Test]
        //public void TestValidationCreate()
        //{
        //    var userService = new UserService(new UserRepositories(),_mapper);

        //    var command = new UserCommand()
        //    {
        //        Name = "teste",
        //        Email = "email@email.com",
        //        Password = "senha123"
        //    };

        //    var result = userService.ValidatesUser(command);

        //    Assert.IsTrue(result.Count == 0);
        //}
        //[Test]
        //public void TestGetAllUsers()
        //{
        //    var userRepository = new UserRepositories();

        //    var result = userRepository.SelectAllUsers() ;

        //    Assert.IsTrue(result.Count()  > 0);
        //}
        //[Test]
        //public void TestGetbyId()
        //{
        //    var userRepository = new UserRepositories();

        //    var result = userRepository.SelectByUserId(4);

        //    Assert.IsTrue(result != null);
        //}
        //[Test]
        //public void TestIsNotAlreadyCreate()
        //{
        //    var userService = new UserService(new UserRepositories(),_mapper);

        //    var command = new UserCommand()
        //    {
        //        Name = "teste",
        //        Email = "email@email.com",
        //        Password = "senha123"
        //    };

        //    var result = userService.ValidatesUser(command);

        //    Assert.IsFalse(result.Count > 0);
        //}
        //[Test]
        //public void TestIsAlreadyCreate()
        //{
        //    var userService = new UserService(new UserRepositories(), _mapper);

        //    var command = new UserCommand()
        //    {
        //        Name = "teste",
        //        Email = "email@email.com",
        //        Password = "senha123"
        //    };

        //    var result = userService.ValidatesUser(command);

        //    Assert.IsTrue(result.Count == 0);
        //}
        //[Test]
        //public void TestValidationUpdate()
        //{
        //    var userService = new UserService(new UserRepositories(), _mapper);

        //    var command = new UpdateUserCommand()
        //    {
        //        Id = 4,
        //        Name = "teste",
        //        Email = "email@email.com",
        //        Password = "senha123"
        //    };

        //    var result = userService.ValidatesUpdateUser(command);

        //    Assert.IsTrue(result.Count == 0);
        //}

        //[Test]
        //public void TestValidationDelete()
        //{
        //    var userService = new UserService(new UserRepositories(), _mapper);

        //    var command = new DeleteUserCommand()
        //    {
        //        UserId = 4
        //    };

        //    var result = userService.ValidaDelete(command);

        //    Assert.IsTrue(result.Count == 0);
        //}

        //[Test]
        //public void TestLogin()
        //{
        //    var userService = new UserService(new UserRepositories(), _mapper);
        //    var userRepository = new UserRepositories();

        //    var guid = Guid.NewGuid().ToString();

        //    var usuario = new Usuario()
        //    {
        //        Nome = "Teste 1234",
        //        Email = $"email{guid.Substring(0, 6)}@email.com",
        //        Senha = guid.Substring(0, 9)
        //    };

        //    userRepository.Insert(usuario);

        //    var result = userService.Login(new LoginCommand() { Email = usuario.Email, Password = usuario.Senha });           

        //    Assert.IsTrue(result.Data as Usuario != null);
        //}

        //[Test]
        //public void TestRepoUpdate()
        //{
        //    var userRepository = new UserRepositories();

        //    var guid = Guid.NewGuid().ToString();

        //    var usuario = new Usuario()
        //    {
        //        Nome = $"Teste{guid.Substring(0, 6)}",
        //        Email = $"email{guid.Substring(0, 6)}@email.com",
        //        Senha = guid.Substring(0, 9)
        //    };

        //    usuario.Id = userRepository.Insert(usuario).Id;
        //    usuario.Email = $"email_email{guid.Substring(0, 6)}@email.com";

        //    userRepository.Update(usuario);

        //    var anotherUser = userRepository.ExistEmail(usuario.Email);

        //    Assert.IsTrue(anotherUser);
        //}

        //[Test]
        //public void TestRepoDelete()
        //{
        //    var userRepository = new UserRepositories();

        //    var guid = Guid.NewGuid().ToString();

        //    var usuario = new Usuario()
        //    {
        //        Nome = $"Teste{guid.Substring(0, 6)}",
        //        Email = $"email{guid.Substring(0, 6)}@email.com",
        //        Senha = guid
        //    };

        //    Usuario newUser = userRepository.Insert(usuario);

        //    Assert.That(() => userRepository.Delete(newUser.Id), Throws.Nothing);
        //}
    }
}
