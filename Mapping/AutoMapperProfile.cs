using AutoMapper;
using NZWalks.API.Models.Domin_Model;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalkRequstDTO, Walk>().ReverseMap();
            CreateMap<WalkDTO, Walk>().ReverseMap();
            CreateMap<DifficultyDTO, Difficulty>().ReverseMap();
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();


        }
    }
}
