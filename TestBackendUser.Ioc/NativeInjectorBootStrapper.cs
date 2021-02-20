using Microsoft.Extensions.DependencyInjection;
using TestBackendUser.Infra.Repository;
using TestBackendUser.Service;

namespace TestBackendUser.Ioc
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddTransient<UserRepositories>();
            services.AddTransient<SecurityService>();
        }
    }
}
