using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtoMotoWebFlowSync.Config;
using OtoMotoWebFlowSync.Model.OtoMoto;
using RestSharp;

namespace OtoMotoWebFlowSync.Services;

public class OtoMotoHttpClient : IOtoMotoHttpClient
{
    private readonly ILogger<WebFlowHttpClient> _logger;
    private readonly OtoMotoConfig _config;

    public OtoMotoHttpClient(ILogger<WebFlowHttpClient> logger, IOptionsMonitor<OtoMotoConfig> config)
    {
        _logger = logger;
        _config = config.CurrentValue;
    }

    public async Task<string?> GetAccessToken()
    {
        var client = new RestClient($"{_config.ApiUrl}/oauth/token");
        var request = new RestRequest();
        request.AddParameter("client_id", _config.ClientId, ParameterType.GetOrPost);
        request.AddParameter("client_secret", _config.ClientSecret, ParameterType.GetOrPost);
        request.AddParameter("grant_type", "password", ParameterType.GetOrPost);
        request.AddParameter("username", _config.Username, ParameterType.GetOrPost);
        request.AddParameter("password", _config.Password, ParameterType.GetOrPost);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

        try
        {
            var response = await client.PostAsync(request);
            return JsonSerializer.Deserialize<OtoMotoAuthResponse>(response.Content).AccessToken;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        return null;
    }
}

public interface IOtoMotoHttpClient
{
    Task<string?> GetAccessToken();
}