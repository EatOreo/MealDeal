using System.Text.Json;
using MealsConsole.Models;

var text = await File.ReadAllTextAsync("/Users/lukas/Documents/CodeProjects/MealDeal/Mealdeal/MealsConsole/chili.json");

var meal = JsonSerializer.Deserialize<JsonRecipe>(text);

Console.WriteLine(meal.Name);