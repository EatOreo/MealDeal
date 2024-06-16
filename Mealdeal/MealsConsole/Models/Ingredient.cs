using MealsConsole.Services;

namespace MealsConsole.Models;

public class Ingredient
{
    private NameList Names { get; } = new();
    
    public Ingredient(string name, params string[] names)
    {
        Names.Add(name);
        Names.AddRange(names);
    }
}