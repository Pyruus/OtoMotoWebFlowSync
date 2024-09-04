using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtoMotoWebFlowSync.Config;
using OtoMotoWebFlowSync.Model.WebFlow;
using RestSharp;

namespace OtoMotoWebFlowSync.Services;

public class WebFlowHttpClient : IWebFlowHttpClient
{
    private readonly ILogger<WebFlowHttpClient> _logger;
    private readonly WebFlowConfig _config;

    public WebFlowHttpClient(ILogger<WebFlowHttpClient> logger, IOptionsMonitor<WebFlowConfig> config)
    {
        _logger = logger;
        _config = config.CurrentValue;
    }

    public async Task<WebFlowCollectionItemsResponse<FieldData>> GetCarBodyTypes()
    {
        var client = new RestClient($"{_config.ApiUrl}/collections/{_config.CarBodyTypesCollectionId}/items");
        var request = new RestRequest();
        request.AddHeader("Authorization", $"Bearer {_config.ApiKey}");
        var response = await client.ExecuteAsync(request);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return JsonSerializer.Deserialize<WebFlowCollectionItemsResponse<FieldData>>(response.Content, options);
    }
    
    public async Task<WebFlowCollectionItemsResponse<FieldData>> GetFuelTypes()
    {
        var client = new RestClient($"{_config.ApiUrl}/collections/{_config.FuelTypesCollectionId}/items");
        var request = new RestRequest();
        request.AddHeader("Authorization", $"Bearer {_config.ApiKey}");
        var response = await client.ExecuteAsync(request);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return JsonSerializer.Deserialize<WebFlowCollectionItemsResponse<FieldData>>(response.Content, options);
    }
    
    public async Task<WebFlowCollectionItemsResponse<FieldData>> GetCarTags()
    {
        var client = new RestClient($"{_config.ApiUrl}/collections/{_config.CarTagsCollectionId}/items");
        var request = new RestRequest();
        request.AddHeader("Authorization", $"Bearer {_config.ApiKey}");
        var response = await client.ExecuteAsync(request);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return JsonSerializer.Deserialize<WebFlowCollectionItemsResponse<FieldData>>(response.Content, options);
    }
    
    public async Task<WebFlowCollectionItemsResponse<FieldData>> GetBrands()
    {
        var client = new RestClient($"{_config.ApiUrl}/collections/{_config.BrandsCollectionId}/items");
        var request = new RestRequest();
        request.AddHeader("Authorization", $"Bearer {_config.ApiKey}");
        var response = await client.ExecuteAsync(request);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        return JsonSerializer.Deserialize<WebFlowCollectionItemsResponse<FieldData>>(response.Content, options);
    }
    
    public async Task<WebFlowCollectionItemsResponse<Car>> GetCars()
    {
        var client = new RestClient($"{_config.ApiUrl}/collections/{_config.CarsCollectionId}/items");
        var request = new RestRequest();
        request.AddHeader("Authorization", $"Bearer {_config.ApiKey}");
        var response = await client.ExecuteAsync(request);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var data = JsonSerializer.Deserialize<WebFlowCollectionItemsResponse<Car>>(response.Content, options);
        return new WebFlowCollectionItemsResponse<Car>();
    }
}

public interface IWebFlowHttpClient
{
    Task<WebFlowCollectionItemsResponse<FieldData>> GetCarBodyTypes();
    Task<WebFlowCollectionItemsResponse<FieldData>> GetFuelTypes();
    Task<WebFlowCollectionItemsResponse<FieldData>> GetCarTags();
    Task<WebFlowCollectionItemsResponse<FieldData>> GetBrands();
    Task<WebFlowCollectionItemsResponse<Car>> GetCars();

}