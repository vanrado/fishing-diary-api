using System.Text.Json;
using AutoMapper;
using FishingDiary.API.Models;

namespace ConsoleUtils;

public class ExternalFisheryJsonFileWriter
{
    private readonly IMapper _mapper;

    public ExternalFisheryJsonFileWriter(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task WriteToFile(List<ExternalFisheryDto> data, string filePath)
    {
        var json = ConvertAndSerialize(data);
        await File.WriteAllTextAsync(filePath, json);
    }

    public string ConvertAndSerialize(List<ExternalFisheryDto> data)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var fisheriesForCreation = _mapper.Map<List<ExternalFisheryDto>, List<FisheryForCreationDto>>(data);

        return JsonSerializer.Serialize(fisheriesForCreation, options);
    }
}
