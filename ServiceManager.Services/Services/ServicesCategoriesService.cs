using AutoMapper;
using ServicesManager.Contracts.Models;
using ServicesManager.Domain.Entities;
using ServicesManager.Domain.IRepositories;
using ServicesManager.Domain.MyExceptions;
using ServicesManager.Services.Abstractions.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesManager.Services.Services
{
    public class ServicesCategoriesService : IServicesCategoriesService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServicesCategoriesService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceCategory>> GetServicesCategories()
        {
            var serviceCategoryEntity = await _repositoryManager.ServicesCategoriesRepository.GetServicesCategories(trackChanges: false);

            return _mapper.Map<IEnumerable<ServiceCategory>>(serviceCategoryEntity);
        }

        public async Task<ServiceCategory> GetServiceCategoryById(Guid id)
        {
            var serviceCategoryEntity = await _repositoryManager.ServicesCategoriesRepository.GetServiceCategory(id, trackChanges: false);

            if (serviceCategoryEntity == null)
            {
                throw new NotFoundException("Category with entered Id does not exsist");
            }

            return _mapper.Map<ServiceCategory>(serviceCategoryEntity);
        }

        public async Task<Guid> CreateServiceCategory(ServiceCategory serviceCategory)
        {
            var serviceCategoryEntity = _mapper.Map<ServiceCategoryEntity>(serviceCategory);

            await _repositoryManager.ServicesCategoriesRepository.CreateServiceCategory(serviceCategoryEntity);

            return serviceCategoryEntity.Id;
        }

        public async Task UpdateServiceCategory(Guid id, ServiceCategory serviceCategory)
        {
            var serviceCategoryEntity = await _repositoryManager.ServicesCategoriesRepository.GetServiceCategory(id, trackChanges: true);
            serviceCategory.Id = id;

            if (serviceCategoryEntity == null)
            {
                throw new NotFoundException("Category with entered Id does not exsist");
            }

            _mapper.Map(serviceCategory, serviceCategoryEntity);

            await _repositoryManager.ServicesCategoriesRepository.UpdateServiceCategory(serviceCategoryEntity);
        }

        public async Task DeleteServiceCategory(Guid id)
        {
            var serviceCategoryEntity = await _repositoryManager.ServicesCategoriesRepository.GetServiceCategory(id, trackChanges: false);

            if (serviceCategoryEntity == null)
            {
                throw new NotFoundException("Category with entered Id does not exsist");
            }

            await _repositoryManager.ServicesCategoriesRepository.DeleteServiceCategory(serviceCategoryEntity);
        }
    }
}
