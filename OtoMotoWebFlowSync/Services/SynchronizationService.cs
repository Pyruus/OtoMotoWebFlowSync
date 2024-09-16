using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtoMotoWebFlowSync.Config;
using OtoMotoWebFlowSync.Model.WebFlow;

namespace OtoMotoWebFlowSync.Services;

public class SynchronizationService : ISynchronizationService
{
    private readonly ILogger<SynchronizationService> _logger;
    private readonly IWebFlowHttpClient _webFlowHttpClient;
    private readonly IOtoMotoHttpClient _otoMotoHttpClient;
    private readonly WebFlowConfig _webFlowConfig;

    public SynchronizationService(ILogger<SynchronizationService> logger, IWebFlowHttpClient webFlowHttpClient, IOtoMotoHttpClient otoMotoHttpClient, IOptionsMonitor<WebFlowConfig> optionsMonitor)
    {
        _logger = logger;
        _webFlowHttpClient = webFlowHttpClient;
        _otoMotoHttpClient = otoMotoHttpClient;
        _webFlowConfig = optionsMonitor.CurrentValue;
    }

    public async Task Run()
    {
        var token = await _otoMotoHttpClient.GetAccessToken();
        int page = 1;
        var adverts = await _otoMotoHttpClient.GetAdverts(token, 500, page);
        while (!adverts.IsLastPage)
        {
            page++;
            var newAdverts = await _otoMotoHttpClient.GetAdverts(token, 500, page);
            adverts.Results.AddRange(newAdverts.Results);
            adverts.IsLastPage = newAdverts.IsLastPage;
        }
        var activeAdverts = adverts.Results?.Where(x => x.Status == "active");

        var carBodies = await _webFlowHttpClient.GetCarBodyTypes();
        var fuelTypes = await _webFlowHttpClient.GetFuelTypes();
        var brands = await _webFlowHttpClient.GetBrands();
        
        var webFlowCars = await _webFlowHttpClient.GetCars();
        
        var carsToInsert = activeAdverts.Where(o => !webFlowCars.Items.Any(w => w.FieldData.Slug == o.Id.ToString())).Select(x => new Car(x, carBodies.Items, fuelTypes.Items, brands.Items));
        var carsToDelete = webFlowCars.Items.Where(w => !activeAdverts.Any(o => w.FieldData.Slug == o.Id.ToString()));
        var carsToUpdate = webFlowCars.Items.Where(w => activeAdverts.Any(o => w.FieldData.Slug == o.Id.ToString()));
        
        foreach (var car in carsToDelete)
        {
            if (car.LastPublished != null)
            {
                await _webFlowHttpClient.UnpublishCar(car.Id);
            }
            await _webFlowHttpClient.DeleteCar(car.Id);
        }

        var idsToPublish = new List<string>();
        foreach (var car in carsToInsert)
        {
            var newId = await _webFlowHttpClient.PostCar(new WebFlowPostCollectionItemRequest<Car>
            {
                CmsLocaleId = _webFlowConfig.CmsLocaleId,
                FieldData = car
            });
            idsToPublish.Add(newId);
        }
        
        foreach (var car in carsToUpdate)
        {
             await _webFlowHttpClient.UpdateCar(new WebFlowPostCollectionItemRequest<Car>
            {
                CmsLocaleId = _webFlowConfig.CmsLocaleId,
                FieldData = new Car(activeAdverts.FirstOrDefault(x => x.Id.ToString() == car.FieldData.Slug), carBodies.Items, fuelTypes.Items, brands.Items)
            }, car.Id);
            idsToPublish.Add(car.Id);
        }
        
        await _webFlowHttpClient.PublishCars(new WebFlowPublishCollectionItemsRequest
        {
            ItemIds = idsToPublish
        });
     }

}

public interface ISynchronizationService
{
    Task Run();
}