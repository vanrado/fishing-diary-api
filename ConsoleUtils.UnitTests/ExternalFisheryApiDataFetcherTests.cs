using ConsoleUtils.Services;

namespace ConsoleUtils.UnitTests;

public class ExternalFisheryApiDataFetcherTests
{
    [Fact]
    public void DeserializeFisheryData_ShouldReturnCorrectData()
    {
        // Arrange
        var fetcher = new ExternalFisheryApiDataFetcher();
        var json = @"
        [
            {
                ""number"": ""3-1420-1-1"",
                ""water_nature"": ""kapr"",
                ""title"": ""VN Evička"",
                ""organization"": ""MO SRZ Banská Štiavnica"",
                ""area"": ""2,3 ha"",
                ""region"": ""Banská Bystrica"",
                ""district"": ""Banská Štiavnica"",
                ""markers"": [
                    {
                        ""lng"": ""18.862302"",
                        ""lat"": ""48.434782""
                    }
                ],
                ""images"": [
                    ""https://www.fishsurfing.com/cdn/fspw-sk-images/31321/a60ff771.webp""
                ]
            }
        ]";

        // Act
        var result = fetcher.DeserializeFisheryData(json);

        // Assert
        Assert.Single(result);
        Assert.Equal("3-1420-1-1", result[0].Number);
        Assert.Equal("kapr", result[0].WaterNature);
    }
}

