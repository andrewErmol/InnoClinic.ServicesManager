using ServicesManager.Domain.Entities;

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
