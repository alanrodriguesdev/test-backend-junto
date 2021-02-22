using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestBackendUser.Domain.Models;

namespace TestBackendUser.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Usuario> VerifyByLoginAndPassword(string email, string password);
        Task<Usuario> Insert(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task<bool> ExistEmail(string email);
        Task<bool> ExistEmailUpdate(string email, int userId);
        Task<Usuario> SelectByUserId(int usuarioId);
        Task<IEnumerable<Usuario>> SelectAllUsers();
        void Delete(int usuarioId);
    }
}
