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
    public class ServicesService : IServicesService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServicesService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        /// <summary>
        /// This method call method from repository to get services
        /// </summary>
        /// <returns>List of services</returns>
        public async Task<IEnumerable<Service>> GetServices()
        {
            var serviceEntity = await _repositoryManager.ServicesRepository.GetServices(trackChanges: false);

            return _mapper.Map<IEnumerable<Service>>(serviceEntity);
        }
        
        /// <summary>
        /// This method call method from repository to get service by id and check by null
        /// </summary>
        /// <param name="id">id to select service</param>
        /// <returns>Service</returns>
        /// <exception cref="NotFoundException">Return not found</exception>
        public async Task<Service> GetServiceById(Guid id)
        {
            var serviceEntity = await _repositoryManager.ServicesRepository.GetService(id, trackChanges: false);

            if (serviceEntity == null)
            {
                throw new NotFoundException("Service with entered Id does not exsist");
            }

            return _mapper.Map<Service>(serviceEntity);
        }

        /// <summary>
        /// This method call method from repository to create service
        /// </summary>
        /// <param name="service">Params for new service</param>
        /// <returns>Id of the new service</returns>
        public async Task<Guid> CreateService(Service service)
        {
            var serviceEntity = _mapper.Map<ServiceEntity>(service);

            await _repositoryManager.ServicesRepository.CreateService(serviceEntity);

            return serviceEntity.Id;
        }

        /// <summary>
        /// This method call method from repository to update service by id and check by null
        /// </summary>
        /// <param name="id">Id to select service</param>
        /// <param name="service">new fields of service</param>
        /// <returns>nothing</returns>
        /// <exception cref="NotFoundException">Return not found</exception>
        public async Task UpdateService(Guid id, Service service)
        {
            var serviceEntity = await _repositoryManager.ServicesRepository.GetService(id, trackChanges: true);
            service.Id = id;

            if (serviceEntity == null)
            {
                throw new NotFoundException("Service with entered Id does not exsist");
            }

            _mapper.Map(service, serviceEntity);

            await _repositoryManager.ServicesRepository.UpdateService(serviceEntity);
        }

        /// <summary>
        /// This method call method from repository to delete service by id and check by null
        /// </summary>
        /// <param name="id">Id to select service</param>
        /// <returns>nothing</returns>
        /// <exception cref="NotFoundException">Return not found</exception>
        public async Task DeleteService(Guid id)
        {
            var serviceEntity = await _repositoryManager.ServicesRepository.GetService(id, trackChanges: false);

            if (serviceEntity == null)
            {
                throw new NotFoundException("Service with entered Id does not exsist");
            }

            await _repositoryManager.ServicesRepository.DeleteService(serviceEntity);
        }
    }
}
