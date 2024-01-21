using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Steuerelement;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<Steuerelement.Classes.FormatInput>();
builder.Services.AddScoped<Steuerelement.Classes.Configs>();
builder.Services.AddScoped<Steuerelement.Classes.TimeFormat>();
builder.Services.AddScoped<Steuerelement.Classes.TimeFormatManagement>();


await builder.Build().RunAsync();
