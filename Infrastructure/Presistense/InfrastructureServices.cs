using Domain.Contracts;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistense.Data;
using Microsoft.EntityFrameworkCore;
using Presistense.Rebository;
using Microsoft.Extensions.Options;
using Presistense.Identity;
using StackExchange.Redis; // ✅ مهم جداً

namespace Presistense
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MetroDbContex>(x =>
                x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddDbContext<UsersDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("IdentityDefaultConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbIntializer>();

            return services;
        }
    }
}
