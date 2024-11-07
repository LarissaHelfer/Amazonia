using API.Application.Dto;
using AutoMapper;
using Project.Entities;

namespace API.Domain.Mapper
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Arvore, ArvoreDto>();
            CreateMap<ArvoreDto, Arvore>();
        }
    }
}
