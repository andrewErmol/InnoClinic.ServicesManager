using ServicesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
