using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PaparaProject.Application.Concrete.Services;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Interfaces.Persistence.Repositories;
using PaparaProject.Application.Interfaces.Services;
using PaparaProject.Application.Utilities.IoC;
using PaparaProject.Application.Utilities.Security.JWT;
using PaparaProject.Infrastructure.Caching.InMemoryCache;
using PaparaProject.Infrastructure.Caching.Redis;
using PaparaProject.Infrastructure.MailService.Hangfire;
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
            services.AddScoped<IUserRepository, EfUserRepository>();
            //services.AddScoped<ICacheService, RedisCacheService>();
            //services.AddScoped<RedisServer>();
            services.AddScoped<IUserRoleRepository, EfUserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IRoleRepository, EfRoleRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IDuesService, DuesService>();
            services.AddTransient<IFlatService, FlatService>();
            services.AddTransient<IFlatTypeService, FlatTypeService>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceTypeService, InvoiceTypeService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddSingleton<ITokenHelper, JwtHelper>();

            ServiceTool.Create(services);
        }
    }
}
