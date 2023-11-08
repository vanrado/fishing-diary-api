using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsoleUtils.Services;
using ConsoleUtils;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddAutoMapper(typeof(ExternalFisheryDataProfile));
        services.AddTransient<ExternalFisheryApiDataFetcher>();
        services.AddTransient<ExternalFisheryJsonFileWriter>();
    })
    .Build();

using var serviceScope = host.Services.CreateScope();
var services = serviceScope.ServiceProvider;

var fetcher = services.GetRequiredService<ExternalFisheryApiDataFetcher>();
var writer = services.GetRequiredService<ExternalFisheryJsonFileWriter>();

var url = ""; // write, don't commit
var data = await fetcher.FetchData(url, 50);
await writer.WriteToFile(data, "fisheries.json");