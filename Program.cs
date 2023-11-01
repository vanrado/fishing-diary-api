using FishingDiaryAPI.DbContexts;
using FishingDiaryAPI.Mocks;
using FishingDiaryAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// register the DbContext on the container, getting the
// connection string from appSettings   
builder.Services.AddDbContext<FisheryDbContext>(o => o.UseSqlite(
    builder.Configuration["ConnectionStrings:FisheryDbContextConnectionString"]));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure authentication
builder.Services.AddAuthentication("Bearer");
                //.AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

// Require authentication for all endpoints
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/weather/{latitude}/{longitude}", async (double latitude, double longitude) => {
    // Mock weather data for demonstration purposes
    var mockWeatherData = new WeatherDto
    {
        Temperature = 25.5f,           // in degrees Celsius
        WindSpeed = 10.2f,             // in meters per second
        CloudCover = 0.4f,             // percentage (0 to 1)
        AtmosphericPressure = 1013.2f, // in hPa
        MoonPhase = "Waxing Gibbous"   // moon phase description
    };
    return Results.Ok(mockWeatherData);
}).Produces<WeatherDto>(200).RequireAuthorization();


// Mock database to store fishing diary entries
var fishingDiaryEntries = MockData.GetMockFishingDiaryEntries();

app.MapGet("/api/diary/entries", () =>
{
    return Results.Ok(fishingDiaryEntries);
}).Produces<List<FishingDiaryEntryDto>>(200);

app.MapPost("/api/diary/entries", async (FishingDiaryEntryDto entry) =>
{
    // Add the new entry to the database
    fishingDiaryEntries.Add(entry);

    return Results.Created($"/entries/{Guid.NewGuid()}", entry);
}).Produces<FishingDiaryEntryDto>(201);


app.MapGet("/api/fisheries", async (FisheryDbContext fisheryDb) =>
{
    return Results.Ok(await fisheryDb.Fisheries.ToListAsync());
}).Produces<List<Fishery>>(200);

app.MapGet("/api/fisheries/{fisheryId}", async (FisheryDbContext fisheryDb, Guid fisheryId) =>
{
    var fisheryObject = await fisheryDb.Fisheries.FirstOrDefaultAsync(fishery => fishery.Id == fisheryId);
    if (fisheryObject != null)
    {
        return Results.Ok(fisheryObject);
    } else
    {
        return Results.NotFound();
    }
}).Produces<Fishery>(200);


// recreate & migrate the database on each run, for demo purposes
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<FisheryDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();