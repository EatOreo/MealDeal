using Deals;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<DealService>();
builder.Services.AddDbContext<DealContext>(options => options.UseSqlite("Data Source=deal.db"));

if (builder.Environment.IsProduction()) builder.Services.AddHostedService<DealGetter>();
else
{
    using var scope = builder.Services.BuildServiceProvider().CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DealContext>();
    if (context.Database.EnsureCreated()) context.Database.Migrate();
    if (!context.Stores.Any()) context.Stores.AddRange(
        new Shop { Id = "9ba51", Name = "netto" },
        new Shop { Id = "DWZE1w", Name = "365discount" }
    );
    context.SaveChanges();
    if (!context.Deals.Any()) scope.ServiceProvider.GetRequiredService<DealService>().UpdateDeals().Wait();
}

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapRazorPages();

app.Run();

