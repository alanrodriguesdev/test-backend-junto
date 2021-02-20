using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBackendUser.CrossCutting;
using TestBackendUser.Domain.Models;
using TestBackendUser.Ioc;
using TestBackendUser.Service.ViewModels;

namespace TestBackendUser.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConnectionStrings.UserConnectionString = Configuration.GetConnectionString("UserConnectionString");

            Environment.SetEnvironmentVariable("Secret", Configuration.GetSection("AppSettings")["Secret"]);
            Environment.SetEnvironmentVariable("ExpiracaoHoras", Configuration.GetSection("AppSettings")["ExpiracaoHoras"]);
            Environment.SetEnvironmentVariable("Emissor", Configuration.GetSection("AppSettings")["Emissor"]);
            Environment.SetEnvironmentVariable("ValidoEm", Configuration.GetSection("AppSettings")["ValidoEm"]);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestBackendUser.Api", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme() 
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(x => x.Name, mo => mo.MapFrom(dest => dest.Nome))
                .ForMember(x => x.Password, mo => mo.MapFrom(dest => dest.Senha));

            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("Secret"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Environment.GetEnvironmentVariable("ValidoEm"),
                    ValidIssuer = Environment.GetEnvironmentVariable("Emissor")
                };
            });
            RegisterServices(services);

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestBackendUser.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
