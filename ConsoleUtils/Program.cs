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

var data = await fetcher.FetchData("https://api.fishsurfing.com/v1/grounds-sk.php?lang=cs&id_user=787386&hash=H10HrZtXyzd1kBfurxc3maKmqSpf9J1Tll6SWePS3hiHN8YGXI54O6Q3pcMncynHo&water_nature=kapr&offset=", 50);
await writer.WriteToFile(data, "fisheries.json");