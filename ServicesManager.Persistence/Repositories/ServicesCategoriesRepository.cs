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
    public class ServicesCategoriesRepository : IServicesCategoriesRepository
    {
        private ServicesDbContext _dbContext;

        public ServicesCategoriesRepository(ServicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ServiceCategoryEntity>> GetServicesCategories(bool trackChanges)
        {
            IQueryable<ServiceCategoryEntity> servicesCategories = !trackChanges
                ? _dbContext.ServicesCategories.AsNoTracking()
                : _dbContext.ServicesCategories;

            return await servicesCategories.ToListAsync();
        }

        public async Task<ServiceCategoryEntity> GetServiceCategory(Guid serviceCategoryId, bool trackChanges)
        {
            ServiceCategoryEntity serviceCategory = !trackChanges
                ? await _dbContext.ServicesCategories.AsNoTracking().FirstOrDefaultAsync(o => o.Id == serviceCategoryId)
                : await _dbContext.ServicesCategories.FirstOrDefaultAsync(o => o.Id == serviceCategoryId);

            return serviceCategory;
        }

        public async Task CreateServiceCategory(ServiceCategoryEntity serviceCategory)
        {
            await _dbContext.ServicesCategories.AddAsync(serviceCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteServiceCategory(ServiceCategoryEntity serviceCategory)
        {
            _dbContext.ServicesCategories.Remove(serviceCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateServiceCategory(ServiceCategoryEntity serviceCategory)
        {
            _dbContext.ServicesCategories.Update(serviceCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
