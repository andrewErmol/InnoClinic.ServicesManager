using Microsoft.EntityFrameworkCore;
using ServicesManager.Domain.Entities;
using ServicesManager.Domain.IRepositories;

namespace ServicesManager.Persistence.Repositories
{
    public class ServicesCategoriesRepository : IServicesCategoriesRepository
    {
        private ServicesDbContext _dbContext;

        public ServicesCategoriesRepository(ServicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method get list of categories from database
        /// </summary>
        /// <param name="trackChanges">For use or don't use method AsNoTracking</param>
        /// <returns>List of Categories</returns>
        public async Task<IEnumerable<ServiceCategoryEntity>> GetServicesCategories(bool trackChanges)
        {
            IQueryable<ServiceCategoryEntity> servicesCategories = !trackChanges
                ? _dbContext.ServicesCategories.AsNoTracking()
                : _dbContext.ServicesCategories;

            return await servicesCategories.ToListAsync();
        }

        /// <summary>
        /// This method get category from database by id
        /// </summary>
        /// <param name="serviceCategoryId">Id for select query to database</param>
        /// <param name="trackChanges">For use or don't use method AsNoTracking</param>
        /// <returns>Category with entered Id</returns>
        public async Task<ServiceCategoryEntity> GetServiceCategory(Guid serviceCategoryId, bool trackChanges)
        {
            ServiceCategoryEntity serviceCategory = !trackChanges
                ? await _dbContext.ServicesCategories.AsNoTracking().FirstOrDefaultAsync(o => o.Id == serviceCategoryId)
                : await _dbContext.ServicesCategories.FirstOrDefaultAsync(o => o.Id == serviceCategoryId);

            return serviceCategory;
        }

        /// <summary>
        /// This method create category from database
        /// </summary>
        /// <param name="serviceCategory">in this parameter storage fields of service</param>
        /// <returns>nothing</returns>
        public async Task CreateServiceCategory(ServiceCategoryEntity serviceCategory)
        {
            await _dbContext.ServicesCategories.AddAsync(serviceCategory);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This method delete category from database
        /// </summary>
        /// <param name="serviceCategory">Entity witch will be deleted</param>
        /// <returns>nothing</returns>
        public async Task DeleteServiceCategory(ServiceCategoryEntity serviceCategory)
        {
            _dbContext.ServicesCategories.Remove(serviceCategory);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// This method update category from database
        /// </summary>
        /// <param name="serviceCategory">Entity with old id, and new fields</param>
        /// <returns>nothing</returns>
        public async Task UpdateServiceCategory(ServiceCategoryEntity serviceCategory)
        {
            _dbContext.ServicesCategories.Update(serviceCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
