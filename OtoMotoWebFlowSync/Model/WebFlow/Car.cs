namespace OtoMotoWebFlowSync.Model.WebFlow;

public class Car : FieldData
{
    public string CarBodyType { get; set; }
    public int YearOfProduction { get; set; }
    public string FuelType { get; set; }
    public string EnginePower { get; set; }
    public string EngineCapacity { get; set; }
    public string MainPhoto { get; set; }
    public List<string> Tags { get; set; }
    public List<string> Gallery { get; set; }
    public bool IsAutomaticGear { get; set; }
    public string Model { get; set; }
    public string ModelVersion { get; set; }
    public string Drive { get; set; }
    public decimal FuelBurnedCity { get; set; }
    public decimal FuelBurnedOutsideCity { get; set; }
    public string CarBodyColor { get; set; }
    public int NoOfDoors { get; set; }
    public int NoOfSeats { get; set; }
    public string OtomotoLink { get; set; }
    public string CarDescription { get; set; }
    public bool IsSold { get; set; }
    public string Brand { get; set; }
    public int Mileage { get; set; }
    public string Vin { get; set; }
    public int Price { get; set; }
    public int PermissibleLoadCapacity { get; set; }
    public int MaximumPermissibleWeight { get; set; }
    public string Engine { get; set; }
}
