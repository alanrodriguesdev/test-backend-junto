using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestBackendUser.Domain.Commands;

namespace TestBackendUser.Service
{
    public class UserBaseService
    {
        public async Task<List<string>> ValidatesUser(UserCommand command)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(command.Name) || command.Name.Length > 100)
                errors.Add("Nome inválido");

            if (string.IsNullOrEmpty(command.Email) || command.Email.Length > 100)
                errors.Add("Email inválido");

            var foo = new EmailAddressAttribute();
            if (!foo.IsValid(command.Email))
                errors.Add("Email inválido");

            if (string.IsNullOrEmpty(command.Password) || command.Password.Length > 100)
                errors.Add("Senha inválida");

            return errors;
        }
        public async Task<List<string>> ValidatesUpdateUser(UpdateUserCommand command,int userId)
        {
            var errors = new List<string>();

            if (userId <= 0)
                errors.Add("UserId inválido");

            if (string.IsNullOrEmpty(command.Name) || command.Name.Length > 100)
                errors.Add("Nome inválido");

            if (string.IsNullOrEmpty(command.Email) || command.Email.Length > 100)
                errors.Add("Email inválido");

            var foo = new EmailAddressAttribute();
            if (!foo.IsValid(command.Email))
                errors.Add("Email inválido");

            if (string.IsNullOrEmpty(command.Password) || command.Password.Length > 100)
                errors.Add("Senha inválida");

            return errors;
        }
        public async Task<List<string>> ValidaDelete(DeleteUserCommand command)
        {
            var errors = new List<string>();

            if (command.UserId == 0)
                errors.Add("UserId inválido");

            return errors;
        }
        public async Task<List<string>> ValidaLogin(LoginCommand command)
        {
            var errors = new List<string>();

            var foo = new EmailAddressAttribute();
            if (!foo.IsValid(command.Email))
                errors.Add("Email inválido");

            if (string.IsNullOrEmpty(command.Password) || command.Password.Length > 100)
                errors.Add("Senha inválida");

            return errors;
        }
        
    }
}
