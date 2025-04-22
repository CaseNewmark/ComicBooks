using AutoMapper;
using ComicBooks.Domain.Entities;
using ComicBooks.SharedDtos;

namespace ComicBooks.ApiService.Services
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            // Map FloorPlan to FloorPlanDto and vice versa
            CreateMap<FloorPlan, FloorPlanDto>().ReverseMap();

            // Map Section to SectionDto and vice versa (assuming SectionDto and Section exist)
            CreateMap<Section, SectionDto>().ReverseMap();
        }
    }
}