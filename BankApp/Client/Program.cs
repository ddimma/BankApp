using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using BankApp.Client.Services;
using Syncfusion.Blazor;

namespace BankApp.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddMudServices();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthService>());
        builder.Services.AddAuthorizationCore();
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddSyncfusionBlazor();
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF5cXmZCekxzWmFZfVpgcl9HZ1ZSTGYuP1ZhSXxXdkZhUH9YcXNWTmVcU0E=");

        await builder.Build().RunAsync();
    }
}

