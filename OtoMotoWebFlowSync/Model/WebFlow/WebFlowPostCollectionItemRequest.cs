using System.Text.Json.Serialization;

namespace OtoMotoWebFlowSync.Model.WebFlow;

public class WebFlowPostCollectionItemRequest<T>
{
    public string CmsLocaleId { get; set; }
    [JsonPropertyName("fieldData")]
    public T FieldData { get; set; }
}