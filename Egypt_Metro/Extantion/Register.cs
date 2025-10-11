using Domain;
using Domain.Modules.Identity;
using Egypt_Metro.MiddleWare;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presistense;
using Presistense.Identity;
using Services;
using Services.Hubs;
using Services.Twilio;
using Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.ErrorModels;
using Twilio.Rest.Api.V2010.Account.Usage.Record;

namespace Egypt_Metro.Extantion
{
    public static class Register
    {
        public static IServiceCollection AddRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var Jwtoption = configuration.GetSection("JWtOptions").Get<JWtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Jwtoption.Issuer,
                   ValidAudience = Jwtoption.Audience,
                   IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                       System.Text.Encoding.UTF8.GetBytes(Jwtoption.SecretKey)
                   )
               };
           });
            services.AddIdentity<AppUsers, IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddSignInManager<SignInManager<AppUsers>>()
    .AddDefaultTokenProviders();


            services.AddApplicationServices(configuration);
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}
