namespace MealsConsole.Models;

public class Ingredient
{
    public List<string> Names { get; set; } = new();
    public List<Ingredient> Substitutes { get; set; } = new();
    
    public Ingredient(string name)
    {
        Names.Add(name.ToLower());
    }
}