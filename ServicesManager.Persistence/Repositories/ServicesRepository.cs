using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServicesManager.Domain.Entities;
using ServicesManager.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesManager.Persistence.Repositories
{
    public class ServicesRepository : IServicesRepository
    {
        private ServicesDbContext _dbContext;

        public ServicesRepository(ServicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method get list of services from database
        /// </summary>
        /// <param name="trackChanges">For use or don't use method AsNoTracking</param>
        /// <returns>List of Services</returns>
        public async Task<IEnumerable<ServiceEntity>> GetServices(bool trackChanges)
        {
            IQueryable<ServiceEntity> services = !trackChanges
                ? _dbContext.Services.AsNoTracking()
                : _dbContext.Services;

            return await services.ToListAsync();
        }

        /// <summary>
        /// This method get service from database by id
        /// </summary>
        /// <param name="serviceCategoryId">Id for request to database</param>
        /// <param name="trackChanges">For use or don't use method AsNoTracking</param>
        /// <returns>Service with entered Id</returns>
        public async Task<ServiceEntity> GetService(Guid serviceId, bool trackChanges)
        {
            ServiceEntity service = !trackChanges
                ? await _dbContext.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == serviceId)
                : await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == serviceId);

            return service;
        }

        /// <summary>
        /// This method create service from database
        /// </summary>
        /// <param name="serviceCategory">in this parameter storage fields of service</param>
        /// <returns>nothing</returns>
        public async Task CreateService(ServiceEntity service)
        {
            await _dbContext.Services.AddAsync(service);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This method delete service from database
        /// </summary>
        /// <param name="serviceCategory">Entity witch will be deleted</param>
        /// <returns>nothing</returns>
        public async Task DeleteService(ServiceEntity service)
        {
            _dbContext.Services.Remove(service);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This method update service from database
        /// </summary>
        /// <param name="serviceCategory">Entity with old id, and new fields</param>
        /// <returns>nothing</returns>
        public async Task UpdateService(ServiceEntity service)
        {
            _dbContext.Services.Update(service);
            await _dbContext.SaveChangesAsync();
        }
    }
}
