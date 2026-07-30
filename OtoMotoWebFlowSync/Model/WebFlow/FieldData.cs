using System.Text.Json.Serialization;

namespace OtoMotoWebFlowSync.Model.WebFlow;

public class FieldData
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }
}