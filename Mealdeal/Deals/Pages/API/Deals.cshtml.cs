using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Deals.Pages;

public class Deals : PageModel
{
    public string? Search { get; set; }
    public void OnGet(string? search)
    {
        Search = search;
    }
}