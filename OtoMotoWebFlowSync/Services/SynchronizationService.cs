using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtoMotoWebFlowSync.Config;
using OtoMotoWebFlowSync.Helpers;
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
        _logger.LogInformation("Otomoto-webflow synchornization starting");
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
        var activeAdverts = adverts.Results?.Where(x => x.Status == "active").ToList();
        
        activeAdverts.Where(advert => advert.Params.Price.GrossNet == "net")
            .ToList()
            .ForEach(advert => advert.Params.Price.Amount *= 1.23);

        var carBodies = await _webFlowHttpClient.GetCarBodyTypes();
        var fuelTypes = await _webFlowHttpClient.GetFuelTypes();
        var brands = await _webFlowHttpClient.GetBrands();
        
        var webFlowCars = await _webFlowHttpClient.GetCars();

        int offset = 0;
        while (webFlowCars.Pagination.Total > offset + 100)
        {
            offset += 100;
            var newWebflowCars =  await _webFlowHttpClient.GetCars(offset);
            webFlowCars.Items.AddRange(newWebflowCars.Items);
        }
        
        var carsToInsert = activeAdverts.Where(o => !webFlowCars.Items.Any(w => w.FieldData.Slug == o.Id.ToString())).Select(x => new CarForInsert(x, carBodies.Items, fuelTypes.Items, brands.Items)).ToList();
        var carsToDelete = webFlowCars.Items.Where(w => !activeAdverts.Any(o => w.FieldData.Slug == o.Id.ToString()) && w.FieldData.IsAutomaticallyInserted).ToList();
        var carsToUpdate = webFlowCars.Items.Where(w => activeAdverts.Any(o => w.FieldData.Slug == o.Id.ToString())).ToList();
        
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
            var newId = await _webFlowHttpClient.PostCar(new WebFlowPostCollectionItemRequest<CarForInsert>
            {
                CmsLocaleId = _webFlowConfig.CmsLocaleId,
                FieldData = car
            });
            idsToPublish.Add(newId);
        }
        
        foreach (var car in carsToUpdate)
        {
            var carToInsert = new CarForInsert(activeAdverts.FirstOrDefault(x => x.Id.ToString() == car.FieldData.Slug),
                carBodies.Items, fuelTypes.Items, brands.Items);
            
            /*if (CarCompareHelper.AreCarsEqual(carToInsert, car.FieldData))
            {
                continue;
            }*/
            
            await _webFlowHttpClient.UpdateCar(new WebFlowPostCollectionItemRequest<CarForInsert>
            {
                CmsLocaleId = _webFlowConfig.CmsLocaleId,
                FieldData = carToInsert
            }, car.Id);
            idsToPublish.Add(car.Id);
        }

        if (idsToPublish.Count > 0)
        {
            await _webFlowHttpClient.PublishCars(new WebFlowPublishCollectionItemsRequest
            {
                ItemIds = idsToPublish
            });
        }
        var currentWebFlowCars = await _webFlowHttpClient.GetCars();
        offset = 0;
        while (currentWebFlowCars.Pagination.Total > offset + 100)
        {
            offset += 100;
            var newWebflowCars =  await _webFlowHttpClient.GetCars(offset);
            currentWebFlowCars.Items.AddRange(newWebflowCars.Items);
        }

        var currentBrands = currentWebFlowCars.Items.Where(i => i.FieldData.Brand != null).Select(i => i.FieldData.Brand).Distinct().ToList();
        
        foreach (var brand in brands.Items.Where(i => !i.IsDraft).Select(b => b.Id).Except(currentBrands))
        {
            var test = brands.Items.Where(i => !i.IsDraft).Select(b => b.Id).Except(currentBrands);
            Console.WriteLine(test.Count());
            if (brand != null) await _webFlowHttpClient.UnpublishBrand(brand); 
        }

        var brandsToPublish = brands.Items.Where(i => i.IsDraft).Select(b => b.Id).Intersect(currentBrands).ToList();
        
        if (brandsToPublish.Count != 0) 
        {
            await _webFlowHttpClient.PublishBrands(new WebFlowPublishCollectionItemsRequest
            {
                ItemIds = brandsToPublish
            }); 
        }
        
        _logger.LogInformation("Finished working");
     }

}

public interface ISynchronizationService
{
    Task Run();
}