using System.Globalization;
using System.Text.Json.Serialization;
using OtoMotoWebFlowSync.Helpers;
using OtoMotoWebFlowSync.Model.OtoMoto;

namespace OtoMotoWebFlowSync.Model.WebFlow;

public class Car : FieldData
{
    [JsonPropertyName("car-body-type")]
    public string? CarBodyType { get; set; }
    [JsonPropertyName("year-of-production")]
    public int? YearOfProduction { get; set; }
    [JsonPropertyName("fuel-type")]
    public string? FuelType { get; set; }
    [JsonPropertyName("engine-power")]
    public string? EnginePower { get; set; }
    [JsonPropertyName("engine-capacity")]
    public string? EngineCapacity { get; set; }
    [JsonPropertyName("main-photo")]
    public Image? MainPhoto { get; set; }
    [JsonPropertyName("tags")]
    public List<string?>? Tags { get; set; }
    [JsonPropertyName("gallery")]
    public List<Image>? Gallery { get; set; }
    [JsonPropertyName("gallery2")]
    public List<Image>? Gallery2 { get; set; }
    [JsonPropertyName("isautomaticgear")]
    public bool IsAutomaticGear { get; set; }
    [JsonPropertyName("model")]
    public string? Model { get; set; }
    [JsonPropertyName("model-version")]
    public string? ModelVersion { get; set; }
    [JsonPropertyName("drive")]
    public string? Drive { get; set; }
    [JsonPropertyName("fuel-burned-city")]
    public decimal? FuelBurnedCity { get; set; }
    [JsonPropertyName("fuel-burned-outside-city")]
    public decimal? FuelBurnedOutsideCity { get; set; }
    [JsonPropertyName("car-body-color")]
    public string? CarBodyColor { get; set; }
    [JsonPropertyName("no-of-doors")]
    public int? NoOfDoors { get; set; }
    [JsonPropertyName("no-of-seats")]
    public int? NoOfSeats { get; set; }
    [JsonPropertyName("otomoto-link")]
    public string? OtomotoLink { get; set; }
    [JsonPropertyName("car-description")]
    public string? CarDescription { get; set; }
    [JsonPropertyName("issold")]
    public bool IsSold { get; set; }
    [JsonPropertyName("brand-2")]
    public string? Brand { get; set; }
    [JsonPropertyName("mileage-2")]
    public int? Mileage { get; set; }
    [JsonPropertyName("vin")]
    public string? Vin { get; set; }
    [JsonPropertyName("price-2")]
    public int? Price { get; set; }
    [JsonPropertyName("permissible-load-capacity")]
    public int? PermissibleLoadCapacity { get; set; }
    [JsonPropertyName("maximum-permissible-weight")]
    public int? MaximumPermissibleWeight { get; set; }
    [JsonPropertyName("engine")]
    public string? Engine { get; set; }
    [JsonPropertyName("isautomaticallyinserted")]
    public bool IsAutomaticallyInserted { get; set; }

    public Car(){}
    
    public Car(Advert advert,
        List<CollectionItem<FieldData>> carBodies,
        List<CollectionItem<FieldData>> fuelTypes,
        List<CollectionItem<FieldData>> brands
        )
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        Name = $"{char.ToUpper(advert.Params?.Make?[0] ?? ' ')}{advert.Params?.Make?[1..]} {char.ToUpper(advert.Params?.Model?[0] ?? ' ')}{advert.Params?.Model?[1..]}";
        Slug = advert.Id.ToString();
        CarBodyType = AdvertToCarMapperHelper.GetBodyTypeId(advert.Params?.BodyType, carBodies);
        YearOfProduction = advert.Params?.Year;
        FuelType = AdvertToCarMapperHelper.GetFuelTypeId(advert.Params?.FuelType, fuelTypes);
        EnginePower = $"{advert.Params?.EnginePower} KM";
        EngineCapacity = $"{advert.Params?.EngineCapacity}  cm3";
        MainPhoto = new Image
        {
            Url = advert.Photos?["1"].LargeSize
                  ?? advert.Photos?["1"].StandardSize
                  ?? advert.Photos?["1"].MediumSize
                  ?? advert.Photos?["1"].SmallSize
                  ?? advert.Photos?["1"].ThumbnailSize
        };
        Gallery = AdvertToCarMapperHelper.MapPhotosToGallery(advert.Photos);
        Gallery2 = AdvertToCarMapperHelper.MapPhotosToGallery(advert.Photos, 25);
        IsAutomaticGear = advert.Params?.Gearbox == "automatic";
        Model = advert.Params?.Model;
        ModelVersion = advert.Params?.Version;
        Drive = AdvertToCarMapperHelper.MapDrive(advert.Params?.Transmission);
        FuelBurnedCity = advert.Params?.UrbanConsumption;
        if (advert.Params?.ExtraUrbanConsumption != null)
            FuelBurnedOutsideCity = decimal.Parse(advert.Params?.ExtraUrbanConsumption);
        CarBodyColor = AdvertToCarMapperHelper.TranslateColor(advert.Params?.Color);
        if (advert.Params?.DoorCount != null) 
            NoOfDoors = int.Parse(advert.Params?.DoorCount);
        if (advert.Params?.NrSeats != null) 
            NoOfSeats = int.Parse(advert.Params?.NrSeats);
        OtomotoLink = advert.Url;
        CarDescription = advert.Description;
        IsSold = false;
        Brand = AdvertToCarMapperHelper.GetBrandId(advert.Params?.Make, brands);
        Mileage = advert.Params?.Mileage;
        Vin = advert.Params?.Vin;
        Price = advert.Params?.Price?.Amount;
        PermissibleLoadCapacity = advert.Params?.MaxWeight;
        MaximumPermissibleWeight = advert.Params?.MaxCargoWeight;
        Engine = AdvertToCarMapperHelper.MapCapacityToEngineVersion(advert.Params?.EngineCapacity);
        IsAutomaticallyInserted = true;
    }
}

public class Image
{
    public string? Url { get; set; }
}