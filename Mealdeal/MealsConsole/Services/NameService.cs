namespace MealsConsole.Services;

public class NameService
{
    public static List<string> AlternativeNames(string name)
    {
        return new List<string> {name + "s", "a " + name};
    }
}