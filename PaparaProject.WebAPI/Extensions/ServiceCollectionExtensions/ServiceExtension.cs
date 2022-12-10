using Microsoft.Extensions.DependencyInjection;
using PaparaProject.Application.Concrete.Services;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.IoC;
using PaparaProject.Application.Utilities.Security.JWT;
using PaparaProject.Infrastructure.Caching.Redis;
using PaparaProject.Persistence.Repositories.EntityFramework;

namespace PaparaProject.WebAPI.Extensions.ServiceCollectionExtensions
{
    public static class ServiceExtension
    {
        public static void ServiceRegistration(this IServiceCollection services)
        {
            
            services.AddScoped<IDuesRepository, EfDuesRepository>();
            services.AddScoped<IFlatRepository, EfFlatRepository>();
            services.AddScoped<IFlatTypeRepository, EfFlatTypeRepository>();
            services.AddScoped<IInvoiceRepository, EfInvoiceRepository>();
            services.AddScoped<IInvoiceTypeRepository, EfInvoiceTypeRepository>();
            services.AddScoped<IMessageRepository, EfMessageRepository>();
            services.AddScoped<IRoleRepository, EfRoleRepository>();
            services.AddScoped<IUserRepository, EfUserRepository>();
            services.AddScoped<IUserRoleRepository, EfUserRoleRepository>();
            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddScoped<RedisServer>();

            services.AddTransient<IDuesService, DuesService>();
            services.AddTransient<IFlatService, FlatService>();
            services.AddTransient<IFlatTypeService, FlatTypeService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceTypeService, InvoiceTypeService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthService, AuthService>();

            services.AddSingleton<ITokenService, TokenHandler>();

            ServiceTool.Create(services);
        }
    }
}
