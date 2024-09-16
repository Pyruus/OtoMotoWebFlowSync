using System.Globalization;
using OtoMotoWebFlowSync.Model.OtoMoto;
using OtoMotoWebFlowSync.Model.WebFlow;

namespace OtoMotoWebFlowSync.Helpers;

public static class AdvertToCarMapperHelper
{
    public static string? MapDrive(string? drive)
    {
        return drive switch
        {
            "front-wheel" => "Na przednie koła",
            "rear-wheel" => "Na tylne koła",
            "all-wheel-lock" => "4x4 (dołączany ręcznie)",
            "all-wheel-permanent" => "4x4 (stały)",
            "all-wheel-auto" => "4x4 (dołączany automatycznie)",
            _ => null
        };
    }

    public static string? MapCapacityToEngineVersion(int? capacity)
    {
        return capacity == null 
            ? null 
            : Math.Round((decimal)capacity / 1000M, 1, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture);
    }

    public static List<Image>? MapPhotosToGallery(Dictionary<string, PhotoResolutions>? photos)
    {
        return photos?.Select(
            photo => new Image()
            {
                Url = photo.Value.LargeSize ?? 
                      photo.Value.StandardSize ?? 
                      photo.Value.MediumSize ?? 
                      photo.Value.SmallSize ?? 
                      photo.Value.ThumbnailSize
            }).ToList();
    }

    public static string? GetFuelTypeId(string? fuelType, List<CollectionItem<FieldData>> fuelTypes)
    {
        if (fuelType == null)
            return null;
        if (fuelType.Contains("diesel")) 
            return fuelTypes.FirstOrDefault(x => x.FieldData.Slug == "diesel")?.Id; 
        if (fuelType.Contains("petrol")) 
            return fuelTypes.FirstOrDefault(x => x.FieldData.Slug == "benzyna")?.Id;
        if (fuelType.Contains("electric"))
            return fuelTypes.FirstOrDefault(x => x.FieldData.Slug == "elektryczny")?.Id;
        if (fuelType.Contains("hybrid")) 
            return fuelTypes.FirstOrDefault(x => x.FieldData.Slug == "hybryda")?.Id;
        return null;
    }
    public static string? GetBrandId(string? brand, List<CollectionItem<FieldData>> brands)
    {
        if (brand == null)
            return null;

        return brands.FirstOrDefault(x => x.FieldData.Slug?.ToLower() == brand.ToLower())?.Id;
    }

    public static string? GetBodyTypeId(string? bodyType, List<CollectionItem<FieldData>> bodyTypes)
    {
        if (bodyType == null)
            return null;
        if (bodyType.Contains("compact")) 
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "kompakt")?.Id; 
        if (bodyType.Contains("panel-van")) 
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "furgon-blaszak")?.Id;
        if (bodyType.Contains("mini"))
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "auta-male")?.Id;
        if (bodyType.Contains("minivan")) 
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "minivan")?.Id;
        if (bodyType.Contains("combi")) 
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "kombi")?.Id;
        if (bodyType.Contains("coupe")) 
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "coupe")?.Id;
        if (bodyType.Contains("sedan")) 
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "sedan")?.Id;
        if (bodyType.Contains("suv")) 
            return bodyTypes.FirstOrDefault(x => x.FieldData.Slug == "suv")?.Id;
        return null;
    }
}