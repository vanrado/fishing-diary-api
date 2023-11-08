using FishingDiaryAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using FishingDiaryAPI.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// register the DbContext on the container, getting the
// connection string from appSettings   
builder.Services.AddDbContext<FisheryDbContext>(o => o.UseSqlite(
    builder.Configuration["ConnectionStrings:FisheryDbContextConnectionString"]));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// add SwaggerGen support with configuration for passing JWT Token with request fired from documentation
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuthNZ",
        new()
        {
            Name = "Authorization",
            Description = "Token-based authentication and authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = ParameterLocation.Header
        });
    options.AddSecurityRequirement(new()
            {
                {
                    new ()
                    {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "TokenAuthNZ" }
                    }, new List<string>()}
            });
});

// Configure authentication
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// register automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ensures that standardized error responses are returned when exception is thrown, according RFC 7231 https://tools.ietf.org/html/rfc7231#section-6.6.1
builder.Services.AddProblemDetails();

var app = builder.Build();

// Require authentication for all endpoints
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "My API V1");
    });
} else
{
    app.UseExceptionHandler();
}

// just auto redirect from HTTP -> HTTPS
app.UseHttpsRedirection();

// register API endpoints
app.RegisterFisheriesEndpoints();
app.RegisterWeatherEndpoints();
app.RegisterFishingDiaryEntries();

// recreate & migrate the database on each run, for demo purposes
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<FisheryDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();
