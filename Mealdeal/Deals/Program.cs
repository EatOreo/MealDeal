using Deals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<DealService>();
builder.Services.AddDbContext<DealContext>(options => options.UseSqlite("Data Source=deal.db"));
builder.Services.AddHostedService<DealGetter>();

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/business/{businessName}", async (DealService dealService, [FromRoute] string businessName) =>
{
    return (await dealService.GetBusiness(businessName)).Id;
});

app.MapGet("/deals/{businessName}", async (DealService dealService, [FromRoute] string businessName) =>
{
    return (await dealService.GetOffersFromBusinessName(businessName)).Select(o => new { o.Name, o.Price });
});

app.MapRazorPages();

app.Run();

