using AutoMapper;
using ModelsToRequest.RequestEntity;
using ServicesManager.Contracts.Models;

namespace ServicesManager.API
{
    public class MappingProfileForRequest : Profile
    {
        public MappingProfileForRequest()
        {
            CreateMap<ServiceRequest, Service>();
            CreateMap<ServiceCategoryRequest, ServiceCategory>();
        }
    }
}
