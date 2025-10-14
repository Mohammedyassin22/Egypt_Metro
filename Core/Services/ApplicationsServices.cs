using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistense.Rebository;
using Services.Twilio;
using ServicesAbstraction;
using ServicesAbstraction.Twilio;
using Shared;

namespace Services
{
    public static class ApplicationsServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICacheRebository, CacheRebository>();
            services.AddScoped<IStationsNameServices, StationsNameServices>();
            services.AddScoped<ISerivcesManager, ServicesManager>();
            services.AddScoped<ISmsService, TwilioServices>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<JWtOptions>(configuration.GetSection("JWtOptions"));

            services.AddSignalR();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMetroService, MetroService>();


            return services;
        }
    }
}
