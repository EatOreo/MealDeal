namespace Deals;

public class Shop
{
    public string Id { get; set; }
    public string Name { get; set; }
    public virtual List<Deal> Deals { get; set; }
}