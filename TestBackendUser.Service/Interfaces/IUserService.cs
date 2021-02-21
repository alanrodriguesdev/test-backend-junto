using System.Threading.Tasks;
using TestBackendUser.Domain.Commands;
using TestBackendUser.Domain.Response;

namespace TestBackendUser.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> Login(LoginCommand command);
        Task<UserResponse> Insert(UserCommand command);
        Task<UserResponse> GetAllUsers();
        Task<UserResponse> GetUserById(int userId);
        Task<UserResponse> Update(UpdateUserCommand command);
        Task<UserResponse> Delete(DeleteUserCommand command);

    }
    
}
