using BankApp.Application.Commands.UserAccount.RegisterUser;
using BankApp.Infrastructure;
using BankApp.Server.Common.JWTToken;
using BankApp.Server.Composition;
using BankApp.Application.Services;
using BankApp.Domain.Models;
using BankApp.Infrastructure.Repositories.CommisionRepository;
using BankApp.Application.Services.CommisionService;
using BankApp.Application.Services.TransactionObserver;
using BankApp.Infrastructure.Adapters;
using BankApp.Shared;
using Microsoft.AspNetCore.Identity;
using BankApp.Infrastructure.Persistence;

namespace BankApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddMediatR(config => {
            config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            config.RegisterServicesFromAssemblies(typeof(RegisterUserCommand).Assembly);
        });
        builder.Services.AddInfrastructure(configuration);
        builder.Services.AddJwtIdentity(configuration);
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddSession();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddCors();
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCekxzWmFZfVpgcl9HZ1ZSTGYuP1ZhSXxXdkZhUH9YcXNWTmVcU0E=");

        builder.Services.AddMemoryCache();
        builder.Services.AddLogging();
        builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericServiceDecorator<>));
        builder.Services.AddScoped(typeof(ICommissionRepository), typeof(CommissionRepository));
        builder.Services.AddScoped<CommisionCalculatorService>();

        builder.Services.AddSingleton<TransactionProcessor>();

        builder.Services.AddScoped<ITransactionObserver, ConsoleTransactionLogger>();
        builder.Services.AddScoped<IAdapter<WalletDto, Wallet>, WalletAdapter>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseCors(builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}

