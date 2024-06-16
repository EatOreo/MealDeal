using MealsConsole.Services;

namespace MealsConsole.Models;

public class GlobalNameSet : HashSet<string>
{
    private static GlobalNameSet? _instance;
    public static GlobalNameSet Instance => _instance ??= new GlobalNameSet();
    private GlobalNameSet() { }
}

public class NameList : List<string>
{
    public new void Add(string name)
    {
        name = name.ToLower();
        var allIngredients = GlobalNameSet.Instance;
        if (allIngredients.Contains(name))
        {
            throw new ArgumentException($"Ingredient {name} already exists.");
        }
        base.Add(name);
        allIngredients.Add(name);
        foreach (var alternativeName in NameService.AlternativeNames(name).Where(alternativeName => !allIngredients.Contains(alternativeName)))
        {
            base.Add(alternativeName);
            allIngredients.Add(alternativeName);
        }
    }
    
    public new bool Remove(string name)
    {
        name = name.ToLower();
        var allIngredients = GlobalNameSet.Instance;
        if (allIngredients.Contains(name))
        {
            allIngredients.Remove(name);
        }
        return base.Remove(name);
    }
    
    public new bool Contains(string name)
    {
        return base.Contains(name.ToLower());
    }
    
    public new void AddRange(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            Add(name);
        }
    }
    
    public new void Clear()
    {
        var allIngredients = GlobalNameSet.Instance;
        foreach (var name in this)
        {
            allIngredients.Remove(name);
        }
        base.Clear();
    }
}