using System.Text.Json.Serialization;

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
    public List<Image?>? Gallery { get; set; }
    [JsonPropertyName("is-automatic-gear")]
    public bool IsAutomaticGear { get; set; }
    [JsonPropertyName("model")]
    public string? Model { get; set; }
    [JsonPropertyName("model-version")]
    public string? ModelVersion { get; set; }
    [JsonPropertyName("drive")]
    public string? Drive { get; set; }
    [JsonPropertyName("fuel-burned-city")]
    public decimal FuelBurnedCity { get; set; }
    [JsonPropertyName("fuel-burned-outside-city")]
    public decimal FuelBurnedOutsideCity { get; set; }
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
    [JsonPropertyName("is-sold")]
    public bool IsSold { get; set; }
    [JsonPropertyName("brand")]
    public string? Brand { get; set; }
    [JsonPropertyName("mileage")]
    public int? Mileage { get; set; }
    [JsonPropertyName("vin")]
    public string? Vin { get; set; }
    [JsonPropertyName("price")]
    public int? Price { get; set; }
    [JsonPropertyName("permissible-load-capacity")]
    public int? PermissibleLoadCapacity { get; set; }
    [JsonPropertyName("maximum-permissible-weight")]
    public int? MaximumPermissibleWeight { get; set; }
    [JsonPropertyName("engine")]
    public string? Engine { get; set; }
}

public class Image
{
    public string Url { get; set; }
}