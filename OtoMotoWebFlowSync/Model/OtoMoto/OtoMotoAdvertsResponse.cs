namespace OtoMotoWebFlowSync.Model.OtoMoto;

public class OtoMotoAdvertsResponse
{
    public List<Advert>? Results { get; set; }
    public bool IsLastPage { get; set; }
    public bool IsFirstPage { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentElements { get; set; }
    public int TotalElements { get; set; }
}