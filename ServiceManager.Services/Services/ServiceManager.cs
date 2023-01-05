using AutoMapper;
using ServicesManager.Domain.IRepositories;
using ServicesManager.Services.Abstractions.IServices;

namespace ServicesManager.Services.Services
{
    public class ServiceManager : IServiceManager
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        private IServicesService _servicesService;
        private IServicesCategoriesService _servicesCategoriesService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IServicesService ServicesService
        {
            get
            {
                if (_servicesService == null)
                    _servicesService = new ServicesService(_repositoryManager, _mapper);
                return _servicesService;
            }
        }

        public IServicesCategoriesService ServicesCategoriesService
        {
            get
            {
                if (_servicesCategoriesService == null)
                    _servicesCategoriesService = new ServicesCategoriesService(_repositoryManager, _mapper);
                return _servicesCategoriesService;
            }
        }
    }
}
