using Domain;
using Egypt_Metro.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Presistense;
using Services;
using Services.Hubs;
using Services.Twilio;
using Shared.ErrorModels;
using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace Egypt_Metro.Extantion
{
    public static class Register
    {
        public static IServiceCollection AddRegister(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();



            services.AddSignalR();
            
            services.AddHttpClient();
            services.AddApplicationServices(configuration);
            
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationError
                        {
                            field = m.Key,
                            errors = m.Value.Errors.Select(error => error.ErrorMessage)
                        });

                    var response = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            services.AddInfrastructer(configuration);
            services.Configure<TwilioSetting>(
                 configuration.GetSection("Twilio")
             );
            return services;
        }
        public static async Task<WebApplication> configurationmiddleware(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                await dbInitializer.InitializeAsync();
            }

            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();
            app.MapHub<NotificationHub>("/notificationHub");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}
