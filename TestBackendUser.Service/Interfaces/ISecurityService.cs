using System.Threading.Tasks;
using TestBackendUser.Domain.Models;

namespace TestBackendUser.Service.Interfaces
{
    public interface ISecurityService
    {
        Task<string> GerarJwt(Usuario usuario);
    }
}
