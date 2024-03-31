namespace MealsConsole.Models;

public class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
    public string Instructions { get; set; }
}