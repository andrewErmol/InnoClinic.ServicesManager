using AutoMapper;
using ServicesManager.Contracts.Models;
using ServicesManager.Domain.Entities;

namespace ServicesManager.Services
{
    public class MappingProfileForEntity : Profile
    {
        public MappingProfileForEntity()
        {
            CreateMap<Service, ServiceEntity>();
            CreateMap<ServiceEntity, Service>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));

            CreateMap<ServiceCategory, ServiceCategoryEntity>();
            CreateMap<ServiceCategoryEntity, ServiceCategory>();
        }
    }
}
