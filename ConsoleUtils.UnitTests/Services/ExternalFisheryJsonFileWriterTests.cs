using AutoMapper;

namespace ConsoleUtils.UnitTests;

public class ExternalFisheryJsonFileWriterTests
{
    [Fact]
    public void ConvertAndSerialize_ShouldReturnCorrectJson()
    {
        // Arrange
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ExternalFisheryDataProfile>()).CreateMapper();
        var writer = new ExternalFisheryJsonFileWriter(mapper);
        var data = new List<ExternalFisheryDto>
        {
            new ExternalFisheryDto
            {
                Number = "3-1420-1-1",
                WaterNature = "kapr",
                Title = "VN Evička",
                Organization = "MO SRZ Banská Štiavnica",
                Area = "2,3 ha",
                Region = "Banská Bystrica",
                District = "Banská Štiavnica",
                Markers = new List<ExternalMarkerDto>
                {
                    new ExternalMarkerDto
                    {
                        Lng = 18.862302m,
                        Lat = 48.434782m
                    }
                },
                Images = new List<string>
                {
                    "https://www.fishsurfing.com/cdn/fspw-sk-images/31321/a60ff771.webp"
                }
            }
        };

        // Act
        var json = writer.ConvertAndSerialize(data);

        // Assert
        Assert.Contains("\"Number\": \"3-1420-1-1\"", json);
        Assert.Contains("\"WaterNature\": \"kapr\"", json);
        Assert.Contains("\"Title\": \"VN Evi\\u010Dka\"", json);
        // ... add more assertions for the other properties
    }

}
