using ServicesManager.Domain.Entities;

namespace ServicesManager.Domain.IRepositories
{
    public interface IServicesCategoriesRepository
    {
        Task<IEnumerable<ServiceCategoryEntity>> GetServicesCategories(bool trackChanges);
        Task<ServiceCategoryEntity> GetServiceCategory(Guid serviceCategoryId, bool trackChanges);
        Task CreateServiceCategory(ServiceCategoryEntity serviceCategory);
        Task DeleteServiceCategory(ServiceCategoryEntity serviceCategory);
        Task UpdateServiceCategory(ServiceCategoryEntity serviceCategory);
    }
}
