using System;
using System.Collections.Generic;
using System.Text;

namespace TestBackendUser.Infra.Repository
{
    public partial class UserRepositories
    {
        #region SELECT's      

        private const string selectEmail = @"
            SELECT COUNT(Id) FROM Usuario
                WHERE email = @email";
        private const string selectEmailUpdate = @"
            SELECT COUNT(Id) FROM Usuario
                WHERE email = @email
                AND Id <> @userId";
        #endregion

        #region INSERT's
        private const string insertUser = @"
                INSERT INTO Usuario (Nome, Email, Senha)                  
                VALUES (@Nome, @Email, @Senha);
                SELECT * FROM Usuario WHERE Id = last_insert_rowid();";
        private const string selecUsuario = @"
            SELECT * FROM Usuario
               WHERE Id = @usuarioId;";
        private const string selecTodosUsuario = @"
            SELECT * FROM Usuario;      ";
        private const string selectUsuarioByEmailAndPasswaord = @" 
                SELECT * FROM Usuario
                WHERE email = @email AND senha = @password;";
        #endregion

        #region UPDATE's
        private const string update = @"
          UPDATE Usuario SET Nome = @Nome, Email = @Email, Senha = @Senha
                WHERE Id = @usuarioId;   ";
        #endregion

        #region DELETE's
        private const string delete = @" DELETE FROM Usuario WHERE Id = @usuarioId;";
        #endregion
    }
}
