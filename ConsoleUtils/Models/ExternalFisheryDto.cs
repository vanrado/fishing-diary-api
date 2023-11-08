using System.Text.Json.Serialization;

namespace ConsoleUtils;

public class ExternalFisheryDto
{
    [JsonPropertyName("number")]
    public string Number { get; set; }

    [JsonPropertyName("water_nature")]
    public string WaterNature { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("organization")]
    public string Organization { get; set; }

    [JsonPropertyName("area")]
    public string Area { get; set; }

    [JsonPropertyName("region")]
    public string Region { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("markers")]
    public List<ExternalMarkerDto> Markers { get; set; }

    [JsonPropertyName("images")]
    public List<string> Images { get; set; }
}
