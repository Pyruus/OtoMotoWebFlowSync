using OtoMotoWebFlowSync.Model.WebFlow;

namespace OtoMotoWebFlowSync.Helpers;

public static class CarCompareHelper
{
    public static bool AreCarsEqual(Car first, Car second)
    {
        if (first.Name != second.Name) return false;
        if (first.Slug != second.Slug) return false;
        if (first.CarBodyType != second.CarBodyType) return false;
        if (first.YearOfProduction != second.YearOfProduction) return false;
        if (first.FuelType != second.FuelType) return false;
        if (first.EnginePower != second.EnginePower) return false;
        if (first.EngineCapacity != second.EngineCapacity) return false;
        if (first.Gallery?.Count != second.Gallery?.Count) return false;
        if (first.Gallery2?.Count != second.Gallery2?.Count) return false;
        if (first.IsAutomaticGear != second.IsAutomaticGear) return false;
        if (first.Model != second.Model) return false;
        if (first.ModelVersion != second.ModelVersion) return false;
        if (first.Drive != second.Drive) return false;
        if (first.FuelBurnedCity != second.FuelBurnedCity) return false;
        if (first.FuelBurnedOutsideCity != second.FuelBurnedOutsideCity) return false;
        if (first.CarBodyColor != second.CarBodyColor) return false;
        if (first.NoOfDoors != second.NoOfDoors) return false;
        if (first.NoOfSeats != second.NoOfSeats) return false;
        if (first.OtomotoLink != second.OtomotoLink) return false;
        if (first.CarDescription != second.CarDescription) return false;
        if (first.IsSold != second.IsSold) return false;
        if (first.Brand != second.Brand) return false;
        if (first.Mileage != second.Mileage) return false;
        if (first.Vin != second.Vin) return false;
        if (first.Price != second.Price) return false;
        if (first.PermissibleLoadCapacity != second.PermissibleLoadCapacity) return false;
        if (first.MaximumPermissibleWeight != second.MaximumPermissibleWeight) return false;
        if (first.Engine != second.Engine) return false;
        return true;
    }
}