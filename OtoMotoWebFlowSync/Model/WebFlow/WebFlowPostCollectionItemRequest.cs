namespace OtoMotoWebFlowSync.Model.WebFlow;

public class WebFlowPostCollectionItemRequest<T>
{
    public string CmsLocaleId { get; set; }
    public T FieldData { get; set; }
}