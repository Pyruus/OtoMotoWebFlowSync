namespace OtoMotoWebFlowSync.Model.WebFlow;

public class WebFlowCollectionItemsResponse<T>
{
    public List<CollectionItem<T>> Items { get; set; }
    public Pagination Pagination { get; set; }
}

public class Pagination
{
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Total { get; set; }
}