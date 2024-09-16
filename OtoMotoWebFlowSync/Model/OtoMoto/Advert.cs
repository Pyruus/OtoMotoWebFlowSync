using System.Text.Json.Serialization;

namespace OtoMotoWebFlowSync.Model.OtoMoto;

public class Advert
{
    public long? Id { get; set; }
    public long? UserId { get; set; }
    public string? Status { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string? CretedAt { get; set; }
    public string? ValidTo { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, PhotoResolutions>? Photos { get; set; }
    public Params? Params { get; set; }
    public int? ImageCollectionId { get; set; }
    public string? NewUsed { get; set; }
    public string? VisibleInProfile { get; set; }
    public string? LastUpdateDate { get; set; }
}

public class Params
{
    public string? HasVin { get; set; }
    public string? HasRegistration { get; set; }
    public string? CepikAuthorization { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Version { get; set; }
    public int? Year { get; set; }
    public int? Mileage { get; set; }
    public int? EngineCapacity { get; set; }
    public string? Vin { get; set; }
    public string? FuelType { get; set; }
    public int? EnginePower { get; set; }
    public string? Gearbox { get; set; }
    public string? Transmission { get; set; }
    public string? ExtraUrbanConsumption { get; set; }
    public string? BluetoothInterface { get; set; }
    public decimal? UrbanConsumption { get; set; }
    public string? Damaged { get; set; }
    public string? BodyType { get; set; }
    public string? DoorCount { get; set; }
    public string? NrSeats { get; set; }
    public string? Color { get; set; }
    public string? ColourType { get; set; }
    public string? Radio { get; set; }
    public string? Rhd { get; set; }
    public Price? Price { get; set; }
    public string? HandsFreeSystem { get; set; }
    public string? UsbIn { get; set; }
    public string? CountryOrigin { get; set; }
    public string? DateRegistration { get; set; }
    public string? Registration { get; set; }
    public string? Registered { get; set; }
    public string? OriginalOwner { get; set; }
    public string? NoAccident { get; set; }
    public string? ServiceRecord { get; set; }
    public string? NavigationSystem { get; set; }
    public string? HistoricalVehicle { get; set; }
    public string? Tuning { get; set; }
    public string? ApprovalForGoods { get; set; }
    public string? Soundsystem { get; set; }
    public string? TouchscreenMonitor { get; set; }
    public string? AirConditioningType { get; set; }
    public string? UpholsteryType { get; set; }
    public string? HeatedSeatDriver { get; set; }
    public string? HeatedSeatPassenger { get; set; }
    public string? ArmrestFront { get; set; }
    public string? ArmrestRear { get; set; }
    public int? MaxCargoWeight { get; set; }
    public int? MaxWeight { get; set; }
}

public class Price
{
    [JsonPropertyName("0")]
    public string? Zero { get; set; }
    [JsonPropertyName("1")]
    public int? Amount { get; set; }
    public string? Currency { get; set; }
    public string? GrossNet { get; set; }
}

public class PhotoResolutions
{
    [JsonPropertyName("2048x1360")]
    public string? LargeSize { get; set; }

    [JsonPropertyName("732x488")]
    public string? MediumSize { get; set; }

    [JsonPropertyName("148x110")]
    public string? ThumbnailSize { get; set; }

    [JsonPropertyName("320x240")]
    public string? SmallSize { get; set; }

    [JsonPropertyName("1280x800")]
    public string? StandardSize { get; set; }
}