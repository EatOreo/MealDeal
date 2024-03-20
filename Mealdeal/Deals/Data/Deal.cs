
using System.ComponentModel.DataAnnotations;

namespace Deals;

public class Deal
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public virtual Store Store { get; set; }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    } 
}