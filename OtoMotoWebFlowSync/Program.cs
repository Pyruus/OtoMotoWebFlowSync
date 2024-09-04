using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OtoMotoWebFlowSync.Config;
using OtoMotoWebFlowSync.Services;

DotNetEnv.Env.Load();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((_, config) =>
    {
        config.AddJsonFile("appsettings.json", false, false);
        config.AddInMemoryCollection(new Dictionary<string, string?>
        {
            { "WebFlowConfig:ApiKey", Environment.GetEnvironmentVariable("WebFlowApiKey") },
            { "WebFlowConfig:ApiUrl", Environment.GetEnvironmentVariable("WebFlowApiUrl") },
            { "WebFlowConfig:CarsCollectionId", Environment.GetEnvironmentVariable("CarsCollectionId") },
            { "WebFlowConfig:CarBodyTypesCollectionId", Environment.GetEnvironmentVariable("CarBodyTypesCollectionId") },
            { "WebFlowConfig:FuelTypesCollectionId", Environment.GetEnvironmentVariable("FuelTypesCollectionId") },
            { "WebFlowConfig:CarTagsCollectionId", Environment.GetEnvironmentVariable("CarTagsCollectionId") },
            { "WebFlowConfig:BrandsCollectionId", Environment.GetEnvironmentVariable("BrandsCollectionId") },
        });
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<WebFlowConfig>(context.Configuration.GetSection("WebFlowConfig"));
        
        services.AddScoped<ISynchronizationService, SynchronizationService>();
        services.AddScoped<IWebFlowHttpClient, WebFlowHttpClient>();
    })
    .Build();
    
await builder.Services.GetRequiredService<ISynchronizationService>().Run();