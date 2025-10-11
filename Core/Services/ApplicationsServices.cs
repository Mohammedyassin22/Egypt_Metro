using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Presistense.Rebository;
using Services.Twilio;
using ServicesAbstraction;
using ServicesAbstraction.Twilio;

namespace Services
{
    public static  class ApplicationsServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddScoped<ICacheRebository, CacheRebository>();
            services.AddScoped<IStationsNameServices, StationsNameServices>();
            services.AddScoped<ISerivcesManager, ServicesManager>();
            services.AddScoped<ISmsService, TwilioServices>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           
            return services;
        }
    }
}
