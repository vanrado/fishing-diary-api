using AutoMapper;
using FishingDiary.API.Entities;
using FishingDiary.API.Models;

namespace FishingDiary.API.Profiles
{
    public class FisheryProfile : Profile
    {
        public FisheryProfile() { 
            CreateMap<Fishery, FisheryDto>();
            CreateMap<FisheryForCreationDto, Fishery>();
            CreateMap<FisheryForUpdate, Fishery>();
            CreateMap<UserFishery, UserFisheryDto>();
        }
    }
}
