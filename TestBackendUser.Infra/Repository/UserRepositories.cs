using Dapper;
using System;
using TestBackendUser.Domain.Models;

namespace TestBackendUser.Infra.Repository
{
    public partial class UserRepositories
    {
        public Usuario VerifyByLoginAndPassword(string email, string password)
        {
            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                return _conn.QueryFirstOrDefault<Usuario>(selectUsuarioByEmailAndPasswaord, new { email, password });
            }
        }
        public Usuario Insert(Usuario usuario)
        {          
            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                int userId =_conn.QueryFirstOrDefault<int>(insertUser, usuario);
                return SelectByUserId(userId);
            }
        }
        public Usuario Update(Usuario usuario)
        {
            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                int usuarioId = usuario.Id;
                _conn.Execute(update, new { usuario.Nome,usuario.Email,usuario.Senha, usuarioId });
                return SelectByUserId(usuarioId);
            }
        }

        public bool ExistEmail(string email)
        {

            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                return _conn.ExecuteScalar<bool>(selectEmail, new { email });
            }
        }
        public Usuario SelectByUserId(int usuarioId)
        {
            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                return _conn.QueryFirstOrDefault<Usuario>(selecUsuario, new { usuarioId });
            }
        }
        public void Delete(int usuarioId)
        {
            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                _conn.Execute(delete, new { usuarioId });
            }
        }
    }
}
