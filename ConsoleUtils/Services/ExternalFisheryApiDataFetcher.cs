using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ConsoleUtils.Services;

public class ExternalFisheryApiDataFetcher
{
    private readonly HttpClient _client = new HttpClient();
    private readonly ILogger<ExternalFisheryApiDataFetcher> _logger;

    public ExternalFisheryApiDataFetcher(ILogger<ExternalFisheryApiDataFetcher> logger)
    {
        _logger = logger;
    }

    public async Task<List<ExternalFisheryDto>> FetchData(string url, int offset)
    {
        var dtos = new List<ExternalFisheryDto>();

        while (true)
        {
            var requestUrl = url + offset;
            _logger.LogInformation("Sending request to {Url}", requestUrl);

            var httpResponse = await _client.GetAsync(requestUrl);
            _logger.LogInformation("Received response. Status code: {StatusCode}, Content length: {ContentLength}",
                httpResponse.StatusCode, httpResponse.Content.Headers.ContentLength);
            var response = await httpResponse.Content.ReadAsStringAsync();
            var fetchedDtos = DeserializeFisheryData(response);

            if (fetchedDtos.Count == 0)
                break;

            dtos.AddRange(fetchedDtos);
            offset += 50;
        }

        return dtos;
    }

    public List<ExternalFisheryDto> DeserializeFisheryData(string json)
    {
        return JsonSerializer.Deserialize<List<ExternalFisheryDto>>(json);
    }
}
