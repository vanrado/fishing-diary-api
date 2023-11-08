using System.Text.Json.Serialization;

namespace ConsoleUtils;

public class ExternalMarkerDto
{
     [JsonPropertyName("lng")]
    public string Lng { get; set; }
     [JsonPropertyName("lat")]
    public string Lat { get; set; }
}
