using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OtoMotoWebFlowSync.Services;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
    {
        config.AddJsonFile("appsettings.json", false, false);
    })
    .ConfigureServices((context, services) =>
    {
        //services.Configure<>(context.Configuration.GetSection(""));
        services.AddScoped<ISynchronizationService, SynchronizationService>();
    })
    .Build();
    
await builder.Services.GetRequiredService<ISynchronizationService>().Run();