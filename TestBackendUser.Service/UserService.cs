using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Domain.Interfaces;
using TestBackendUser.Domain.Models;
using TestBackendUser.Domain.Response;
using TestBackendUser.Service.Interfaces;
using TestBackendUser.Service.ViewModels;

namespace TestBackendUser.Service
{
    public class UserService : UserBaseService, IUserService
    {
        private readonly IUserRepository _userRepositories;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepositories, IMapper mapper)
        {
            _userRepositories = userRepositories;
            _mapper = mapper;
        }
        public async Task<UserResponse> Login(LoginCommand command)
        {
            try
            {
                var errors = await ValidaLogin(command);
                Usuario user = null;

                if (errors.Count == 0)
                    user = await _userRepositories.VerifyByLoginAndPassword(command.Email, command.Password);

                if (user == null)
                {
                    errors.Add("Usuário ou senha inválidos");
                    return new UserResponse(false, null, errors);
                }

                return new UserResponse(true, user, errors);
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }

        }
        public async Task<UserResponse> Insert(UserCommand command)
        {
            try
            {
                var errors = await ValidatesUser(command);

                if (await _userRepositories.ExistEmail(command.Email))
                    errors.Add("Este login já foi cadastrado no sistema");

                if (errors.Count == 0)
                {
                    Usuario usuario = new Usuario() { Email = command.Email, Nome = command.Name, Senha = command.Password };
                    var newUser = await _userRepositories.Insert(usuario);
                    return new UserResponse(newUser != null, Mapper(newUser), errors);
                }

                return new UserResponse(false, null, errors);
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }

        }
        public async Task<UserResponse> GetAllUsers()
        {
            try
            {
                var users = await _userRepositories.SelectAllUsers();
                List<UpdateUsuarioViewModel> usuarios = new List<UpdateUsuarioViewModel>();
                if (users != null)
                {
                    foreach (var item in users)
                    {
                        usuarios.Add(Mapper(item));
                    }

                    return new UserResponse(true, usuarios, new List<string>());
                }


                return new UserResponse(false, null, new List<string>());
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }

        }
        public async Task<UserResponse> GetUserById(int userId)
        {
            try
            {
                var user = await _userRepositories.SelectByUserId(userId);
                List<UpdateUsuarioViewModel> usuarios = new List<UpdateUsuarioViewModel>();
                if (user != null)
                    return new UserResponse(user != null, Mapper(user), new List<string>());

                return new UserResponse(false, null, new List<string>());
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }
        }
        public async Task<UserResponse> Update(UpdateUserCommand command, int userId)
        {
            try
            {
                var errors = await ValidatesUpdateUser(command, userId);


                if (await _userRepositories.ExistEmailUpdate(command.Email, userId))
                    errors.Add("Este email já foi cadastrado no sistema");

                if (errors.Count == 0)
                {
                    var usuario = new Usuario()
                    {
                        Id = userId,
                        Nome = command.Name,
                        Email = command.Email,
                        Senha = command.Password
                    };


                    Usuario updateUser = await _userRepositories.Update(usuario);
                    return new UserResponse(updateUser != null, Mapper(updateUser, true), errors);
                }

                return new UserResponse(false, null, errors);
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }
        }
        public async Task<UserResponse> Delete(DeleteUserCommand command)
        {
            try
            {
                var errors = await ValidaDelete(command);

                if (await _userRepositories.SelectByUserId(command.UserId) == null)
                    errors.Add("Este usuário não existe");

                if (errors.Count == 0)
                {
                    _userRepositories.Delete(command.UserId);
                    return new UserResponse(true, null, errors);
                }

                return new UserResponse(false, null, errors);
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }

        }
        private UpdateUsuarioViewModel Mapper(Usuario usuario, bool isUpdate = false)
        {
            if (!isUpdate)
                return _mapper.Map<UpdateUsuarioViewModel>(usuario);
            else
                return _mapper.Map<UpdateUsuarioViewModel>(usuario);
        }
    }
}
