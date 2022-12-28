using ServicesManager.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesManager.Services.Abstractions.IServices
{
    public interface IServicesService
    {
        Task<IEnumerable<Service>> GetServices();
        Task<Service> GetServiceById(Guid id);
        Task<Guid> CreateService(Service service);
        Task DeleteService(Guid id);
        Task UpdateService(Guid id, Service service);
    }
}
