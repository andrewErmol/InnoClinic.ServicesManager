using ServicesManager.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
