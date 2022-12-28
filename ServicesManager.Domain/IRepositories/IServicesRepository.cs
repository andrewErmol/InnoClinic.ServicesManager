using ServicesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesManager.Domain.IRepositories
{
    public interface IServicesRepository
    {
        Task<IEnumerable<ServiceEntity>> GetServices(bool trackChanges);
        Task<ServiceEntity> GetService(Guid serviceId, bool trackChanges);
        Task CreateService(ServiceEntity service);
        Task DeleteService(ServiceEntity service);
        Task UpdateService(ServiceEntity service);
    }
}
