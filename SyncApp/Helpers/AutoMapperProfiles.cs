using AutoMapper;
using SyncApp.Models;
using SyncApp.Models.ViewModels;
using System;

namespace SyncApp.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<PlatformViewModel, Platform>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => (!src.UpdatedAt.HasValue) ? src.LastUpdate : src.UpdatedAt))
                .ForMember(dest => dest.Well, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<WellViewModel, Well>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => (!src.UpdatedAt.HasValue) ? src.LastUpdate : src.UpdatedAt))
                .ReverseMap();
        }
    }
}
