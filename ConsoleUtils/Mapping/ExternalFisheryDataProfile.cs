using AutoMapper;
using FishingDiary.API.Models;

namespace ConsoleUtils;

public class ExternalFisheryDataProfile: Profile
{
    public ExternalFisheryDataProfile()
    {
        CreateMap<ExternalFisheryDto, FisheryForCreationDto>();
        CreateMap<ExternalMarkerDto, MarkerDto>();
    }
}
