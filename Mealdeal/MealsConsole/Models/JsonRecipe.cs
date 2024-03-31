namespace MealsConsole.Models;

public class JsonRecipe
{
    public string Name { get; set; }
    public Dictionary<string, string> Ingredients { get; set; } = new();
    public string Instructions { get; set; }
}