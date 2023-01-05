using ServicesManager.Contracts.Models;

namespace ServicesManager.Services.Abstractions.IServices
{
    public interface IServicesCategoriesService
    {
        Task<IEnumerable<ServiceCategory>> GetServicesCategories();
        Task<ServiceCategory> GetServiceCategoryById(Guid id);
        Task<Guid> CreateServiceCategory(ServiceCategory serviceCategory);
        Task DeleteServiceCategory(Guid id);
        Task UpdateServiceCategory(Guid id, ServiceCategory serviceCategory);
    }
}
