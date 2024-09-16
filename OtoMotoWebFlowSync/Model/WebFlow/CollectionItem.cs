namespace OtoMotoWebFlowSync.Model.WebFlow;

public class CollectionItem<T>
{
    public string? Id { get; set; }
    public string? CmsLocaleId { get; set; }
    public DateTime? LastPublished { get; set; }
    public DateTime? LastUpdated { get; set; } 
    public DateTime? CreatedOn { get; set; } 
    public bool IsArchived { get; set; } 
    public bool IsDraft { get; set; } 
    public T FieldData { get; set; } 
}