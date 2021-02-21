using Microsoft.Extensions.DependencyInjection;
using TestBackendUser.Domain.Interfaces;
using TestBackendUser.Infra.Repository;
using TestBackendUser.Service;
using TestBackendUser.Service.Interfaces;

namespace TestBackendUser.Ioc
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepositories>();
            services.AddScoped<ISecurityService, SecurityService>();
        }
    }
}
