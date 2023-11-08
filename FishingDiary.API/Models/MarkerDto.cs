using System.Text.Json.Serialization;

namespace FishingDiary.API.Models;

public class MarkerDto
{
    [JsonPropertyName("lng")]
    public string Lng { get; set; }

    [JsonPropertyName("lat")]
    public string Lat { get; set; }
}
