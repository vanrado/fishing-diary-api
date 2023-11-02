using AutoMapper;
using FishingDiaryAPI.Entities;
using FishingDiaryAPI.Models;

namespace FishingDiaryAPI.Profiles
{
    public class FisheryProfile : Profile
    {
        public FisheryProfile() { 
            CreateMap<Fishery, FisheryDto>();
            CreateMap<FisheryForCreationDto, Fishery>();
        }
    }
}
