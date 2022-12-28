using ServicesManager.Domain.IRepositories;

namespace ServicesManager.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private ServicesDbContext _servicesDbContext;
        private IServicesRepository _servicesRepository;
        private IServicesCategoriesRepository _servicesCategoriesRepository;

        public RepositoryManager(ServicesDbContext serviceDbContext)
        {
            _servicesDbContext = serviceDbContext;
        }

        public IServicesRepository ServicesRepository
        {
            get
            {
                if (_servicesRepository == null)
                    _servicesRepository = new ServicesRepository(_servicesDbContext);
                return _servicesRepository;
            }
        }

        public IServicesCategoriesRepository ServicesCategoriesRepository
        {
            get
            {
                if (_servicesCategoriesRepository == null)
                    _servicesCategoriesRepository = new ServicesCategoriesRepository(_servicesDbContext);
                return _servicesCategoriesRepository;
            }
        }
    }
}
