using Deal;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<DealService>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/business/{businessName}", async (DealService dealService, [FromRoute] string businessName) =>
{
    return (await dealService.GetBusiness(businessName)).Id;
});

app.MapGet("/deals/{businessName}", async (DealService dealService, [FromRoute] string businessName) =>
{
    return (await dealService.GetOfferFromBusinessName(businessName)).Offers.Select(o => new { o.Name, o.Price });
});

app.MapRazorPages();

app.Run();

