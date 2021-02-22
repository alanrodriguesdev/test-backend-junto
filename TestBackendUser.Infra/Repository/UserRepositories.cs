using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestBackendUser.Domain.Interfaces;
using TestBackendUser.Domain.Models;

namespace TestBackendUser.Infra.Repository
{
    public partial class UserRepositories : IUserRepository
    {
        public async Task<Usuario> VerifyByLoginAndPassword(string email, string password)
        {
            using var _conn = ConnectionFactory.GetOpenConnection();
                return await _conn.QueryFirstOrDefaultAsync<Usuario>(selectUsuarioByEmailAndPasswaord, new { email, password });

        }
        public async Task<Usuario> Insert(Usuario usuario)
        {
            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                int userId = await _conn.QueryFirstOrDefaultAsync<int>(insertUser, usuario);
                return await SelectByUserId(userId);
            }
        }
        public async Task<Usuario> Update(Usuario usuario)
        {
            using (var _conn = ConnectionFactory.GetOpenConnection())
            {
                int usuarioId = usuario.Id;
                await _conn.ExecuteAsync(update, new { usuario.Nome, usuario.Email, usuario.Senha, usuarioId });
                return await SelectByUserId(usuarioId);
            }
        }
        public async Task<bool> ExistEmail(string email)
        {

            using var _conn = ConnectionFactory.GetOpenConnection();            
                return await _conn.ExecuteScalarAsync<bool>(selectEmail, new { email });            
        }
        public async Task<bool> ExistEmailUpdate(string email,int userId)
        {

            using var _conn = ConnectionFactory.GetOpenConnection();
            return await _conn.ExecuteScalarAsync<bool>(selectEmailUpdate, new { email,userId });
        }
        public async Task<Usuario> SelectByUserId(int usuarioId)
        {
            using var _conn = ConnectionFactory.GetOpenConnection();            
                return await _conn.QueryFirstOrDefaultAsync<Usuario>(selecUsuario, new { usuarioId });
            
        }
        public async Task<IEnumerable<Usuario>> SelectAllUsers()
        {
            using (var _conn = ConnectionFactory.GetOpenConnection())            
                return await _conn.QueryAsync<Usuario>(selecTodosUsuario);
            
        }
        public async void Delete(int usuarioId)
        {
            using var _conn = ConnectionFactory.GetOpenConnection();            
                await _conn.ExecuteAsync(delete, new { usuarioId });
            
        }
    }
}
