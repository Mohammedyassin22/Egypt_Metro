using Microsoft.EntityFrameworkCore;
using Domain;
using Presistense;
using Presistense.Data;
using System.Globalization;
using ServicesAbstraction;
using Domain.Contracts;
using AutoMapper;
using Services.Profile;
using Microsoft.Extensions.DependencyInjection;

namespace Egypt_Metro
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddAutoMapper(typeof(StationsNameServices).Assembly);


            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IStationsNameServices, StationsNameServices>();

            
            builder.Services.AddDbContext<MetroDbContex>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDbInitializer, DbIntializer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
