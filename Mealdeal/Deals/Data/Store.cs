namespace Deals;

public class Store
{
    public string Id { get; set; }
    public string Name { get; set; }
    public virtual List<Deal> Deals { get; set; }
}