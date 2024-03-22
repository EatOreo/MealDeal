namespace Deals;

public class Deal
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string UnitType { get; set; }
    public float UnitsFrom { get; set; }
    public float UnitsTo { get; set; }
    public string SmallImage { get; set; }
    public string BigImage { get; set; }
    public string ShopLink { get; set; }
    public virtual Shop Shop { get; set; }
}