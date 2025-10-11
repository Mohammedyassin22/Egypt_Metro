using Microsoft.EntityFrameworkCore;
using Domain;
using Presistense;
using Presistense.Data;
using ServicesAbstraction;
using Domain.Contracts;
using Services.Profile;
using Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using Services.Twilio;
using ServicesAbstraction.Twilio;
using Services.Hubs;
using Presistense.Rebository;
using Egypt_Metro.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using Microsoft.Extensions.Configuration;
using Egypt_Metro.Extantion;

namespace Egypt_Metro
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRegister(builder.Configuration);


            var app = builder.Build();
            await app.configurationmiddleware();
            await app.RunAsync();
        }
    }
}

