using AutoMapper;
using ServicesManager.Contracts.Models;
using ServicesManager.Domain.Entities;
using ServicesManager.Domain.IRepositories;
using ServicesManager.Domain.MyExceptions;
using ServicesManager.Services.Abstractions.IServices;

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

        /// <summary>
        /// This method call method from repository to get categories
        /// </summary>
        /// <returns>List of categories</returns>
        public async Task<IEnumerable<ServiceCategory>> GetServicesCategories()
        {
            var serviceCategoryEntity = await _repositoryManager.ServicesCategoriesRepository.GetServicesCategories(trackChanges: false);

            return _mapper.Map<IEnumerable<ServiceCategory>>(serviceCategoryEntity);
        }

        /// <summary>
        /// This method call method from repository to get category by id and check by null
        /// </summary>
        /// <param name="id">id to select category</param>
        /// <returns>Category</returns>
        /// <exception cref="NotFoundException">Return not found</exception>
        public async Task<ServiceCategory> GetServiceCategoryById(Guid id)
        {
            var serviceCategoryEntity = await _repositoryManager.ServicesCategoriesRepository.GetServiceCategory(id, trackChanges: false);

            if (serviceCategoryEntity == null)
            {
                throw new NotFoundException("Category with entered Id does not exsist");
            }

            return _mapper.Map<ServiceCategory>(serviceCategoryEntity);
        }

        /// <summary>
        /// This method call method from repository to create category
        /// </summary>
        /// <param name="service">Params for new category</param>
        /// <returns>Id of the new category</returns>
        public async Task<Guid> CreateServiceCategory(ServiceCategory serviceCategory)
        {
            var serviceCategoryEntity = _mapper.Map<ServiceCategoryEntity>(serviceCategory);

            await _repositoryManager.ServicesCategoriesRepository.CreateServiceCategory(serviceCategoryEntity);

            return serviceCategoryEntity.Id;
        }

        /// <summary>
        /// This method call method from repository to update category by id and check by null
        /// </summary>
        /// <param name="id">Id to select category</param>
        /// <param name="service">new fields of category</param>
        /// <returns>nothing</returns>
        /// <exception cref="NotFoundException">Return not found</exception>
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

        /// <summary>
        /// This method call method from repository to delete category by id and check by null
        /// </summary>
        /// <param name="id">Id to select category</param>
        /// <returns>nothing</returns>
        /// <exception cref="NotFoundException">Return not found</exception>
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
