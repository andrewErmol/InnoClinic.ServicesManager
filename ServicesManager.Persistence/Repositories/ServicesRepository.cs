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
        public async Task<IEnumerable<ServiceEntity>> GetServices(bool trackChanges)
        {
            IQueryable<ServiceEntity> services = !trackChanges
                ? _dbContext.Services.AsNoTracking()
                : _dbContext.Services;

            return await services.ToListAsync();
        }

        public async Task<ServiceEntity> GetService(Guid serviceId, bool trackChanges)
        {
            ServiceEntity service = !trackChanges
                ? await _dbContext.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == serviceId)
                : await _dbContext.Services.FirstOrDefaultAsync(s => s.Id == serviceId);

            return service;
        }

        public async Task CreateService(ServiceEntity service)
        {
            await _dbContext.Services.AddAsync(service);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteService(ServiceEntity service)
        {
            _dbContext.Services.Remove(service);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateService(ServiceEntity service)
        {
            _dbContext.Services.Update(service);
            await _dbContext.SaveChangesAsync();
        }
    }
}
