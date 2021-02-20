using AutoMapper;
using System;
using System.Collections.Generic;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Domain.Models;
using TestBackendUser.Domain.Response;
using TestBackendUser.Infra.Repository;
using TestBackendUser.Service.ViewModels;

namespace TestBackendUser.Service
{
    public class UserService : UserBaseService
    {
        private readonly UserRepositories _userRepositories;
        IMapper _mapper;
        public UserService(UserRepositories userRepositories, IMapper mapper)
        {
            _userRepositories = userRepositories;
            _mapper = mapper;
        }
        public UserResponse Login(LoginCommand command)
        {
            try
            {
                var errors = ValidaLogin(command);
                Usuario user = null;

                if (errors.Count == 0)
                    user = _userRepositories.VerifyByLoginAndPassword(command.Email, command.Password);

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
        public UserResponse Insert(UserCommand command)
        {
            try
            {
                var errors = ValidatesUser(command);

                if (_userRepositories.ExistEmail(command.Email))
                    errors.Add("Este login já foi cadastrado no sistema");

                if (errors.Count == 0)
                {
                    Usuario newUser = _userRepositories.Insert(new Usuario() { Email = command.Email, Nome = command.Email, Senha = command.Password });
                    return new UserResponse(newUser != null ? true : false, Mapper(newUser), errors);
                }

                return new UserResponse(false, null, errors);
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }        
           
        }
        public UserResponse Update(UserCommand command)
        {
            try
            {
                var errors = ValidatesUser(command);

                var usuario = new Usuario()
                {
                    Id = command.Id,
                    Email = command.Email,
                    Nome = command.Email,
                    Senha = command.Password
                };

                if (errors.Count == 0)
                {
                    Usuario updateUser = _userRepositories.Update(usuario);
                    return new UserResponse(updateUser != null ? true : false, Mapper(updateUser), errors);
                }

                return new UserResponse(false, null, errors);
            }
            catch (Exception ex)
            {
                return new UserResponse(false, ex, new List<string>());
            }            
        }
        public UserResponse Delete(DeleteUserCommand command)
        {
            try
            {
                var errors = ValidaDelete(command);

                if (_userRepositories.SelectByUserId(command.UserId) == null)
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
        private UsuarioViewModel Mapper(Usuario usuario)
        {
            return _mapper.Map<UsuarioViewModel>(usuario);
        }
    }
}
