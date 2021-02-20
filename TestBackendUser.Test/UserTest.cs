using AutoMapper;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using TestBackendUser.CrossCutting;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Infra.Repository;
using TestBackendUser.Service;

namespace TestBackendUser.Test
{
    public class UserTest
    {
        public IConfiguration Configuration { get; set; }
        public IMapper _mapper { get; set; }


        [SetUp]
        public void Setup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            string filename = @"TestBackendUser.DataBase.db";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + filename;
            
            ConnectionStrings.UserConnectionString = Configuration.GetConnectionString("UserConnectionString").Replace("@path", $"{filePath}");// @"Data Source=" + filePath + "";
        }

        [Test]
        public void TestValidationCreate()
        {
            var userService = new UserService(new UserRepositories(),_mapper);

            var command = new UserCommand()
            {
                Name = "teste",
                Email = "email@email.com",
                Password = "senha123"
            };

            var result = userService.ValidatesUser(command);

            Assert.IsTrue(result.Count == 0);
        }
    }
}
