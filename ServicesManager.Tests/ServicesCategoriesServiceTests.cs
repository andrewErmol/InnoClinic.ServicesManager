using AutoMapper;
using FluentAssertions;
using Moq;
using ServicesManager.Contracts.Models;
using ServicesManager.Domain.Entities;
using ServicesManager.Domain.IRepositories;
using ServicesManager.Domain.MyExceptions;
using ServicesManager.Services;
using ServicesManager.Services.Abstractions.IServices;
using ServicesManager.Services.Services;
using Xunit;

namespace ServicesManager.Tests
{
    public class ServicesCategoriesTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ServicesCategoriesTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfileForEntity());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _repositoryManagerMock = new Mock<IRepositoryManager>();
            _serviceManager = new ServiceManager(_repositoryManagerMock.Object, _mapper);


        }

        private List<ServiceCategoryEntity> _testServices = new List<ServiceCategoryEntity>
        {
            new ServiceCategoryEntity
            {
                Id = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Name = "CategoryName",
                TimeSlotSizeInMinutes = 30
            },
            new ServiceCategoryEntity
            {
                Id = new Guid("e3d934ae-4002-41b7-8dcf-99709425f227"),
                Name = "String",
                TimeSlotSizeInMinutes = 50
            },
        };

        [Fact]
        public async Task GetServicesCategories_ServicesCategoriesExist_ReturnsListOfServicesCategories()
        {
            // Arrange
            List<ServiceCategoryEntity> servicesCategories = new List<ServiceCategoryEntity>();
            servicesCategories.Add(_testServices[0]);
            servicesCategories.Add(_testServices[1]);

            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServicesCategories(false))
                .ReturnsAsync(servicesCategories);

            // Act
            var result = await _serviceManager.ServicesCategoriesService.GetServicesCategories();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveSameCount(servicesCategories);
            result.Should().BeEquivalentTo(servicesCategories);
        }

        [Fact]
        public async Task GetServicesCategories_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServicesCategories(false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.GetServicesCategories();

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task GetServiceCategoryById_ServiceCategoryExists_ReturnsServiceCategory()
        {
            // Arrange
            ServiceCategoryEntity serviceCategory = _testServices[0];

            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), false))
                .ReturnsAsync(serviceCategory);

            // Act
            var result = await _serviceManager.ServicesCategoriesService.GetServiceCategoryById(serviceCategory.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(serviceCategory);
        }

        [Fact]
        public async Task GetServiceCategoryById_ServiceCategoryDoesNotExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Category with entered Id does not exsist";
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), false))
                .ReturnsAsync((ServiceCategoryEntity)null);

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.GetServiceCategoryById(Guid.NewGuid());

            // Assert
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task GetServiceCategoryBiId_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.GetServiceCategoryById(Guid.NewGuid());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task CreateServiceCategory_ServiceCategoryValid_ReturnsServicesCategoryId()
        {
            // Arrange
            ServiceCategory serviceCategory = new ServiceCategory
            {
                Name = "category",
                TimeSlotSizeInMinutes = 45,
            };

            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.CreateServiceCategory(It.IsAny<ServiceCategoryEntity>()))
                .Callback((ServiceCategoryEntity serviceEntity) =>
                {
                    serviceEntity.Id = Guid.NewGuid();
                });

            // Act
            var result = await _serviceManager.ServicesCategoriesService.CreateServiceCategory(serviceCategory);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task CreateServiceCategory_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.CreateServiceCategory(It.IsAny<ServiceCategoryEntity>()))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.CreateServiceCategory(new ServiceCategory());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task UpdateServiceCategory_ServiceCategoryValid_UpdateServiceCategory()
        {
            // Arrange
            ServiceCategoryEntity serviceCategoryEntity = _testServices[0];
            ServiceCategory serviceCategory = new ServiceCategory
            {
                Name = "serviceCategory",
                TimeSlotSizeInMinutes = 35
            };

            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), true))
                .ReturnsAsync(serviceCategoryEntity);
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.UpdateServiceCategory(It.IsAny<ServiceCategoryEntity>()));

            // Act
            await _serviceManager.ServicesCategoriesService.UpdateServiceCategory(serviceCategory.Id, serviceCategory);
        }

        [Fact]
        public async Task UpdateServiceCategory_ServiceCategoryDoesNotExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Category with entered Id does not exsist";

            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), true))
                .ReturnsAsync((ServiceCategoryEntity)null);

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.UpdateServiceCategory(Guid.NewGuid(), new ServiceCategory());

            // Assert
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task UpdateServiceCategory_DatabaseError_ThrowsException()
        {
            // Arrange
            ServiceCategoryEntity serviceCategory = _testServices[0];

            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), true))
                .ReturnsAsync(serviceCategory);
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.UpdateServiceCategory(It.IsAny<ServiceCategoryEntity>()))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.UpdateServiceCategory(Guid.NewGuid(), new ServiceCategory());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task DeleteServiceCategory_ServiceCategoryExists_DeleteServiceCategory()
        {
            // Arrange
            ServiceCategoryEntity serviceCategory = _testServices[0];

            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), false))
                .ReturnsAsync(serviceCategory);
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.DeleteServiceCategory(It.IsAny<ServiceCategoryEntity>()));

            // Act
            await _serviceManager.ServicesCategoriesService.DeleteServiceCategory(serviceCategory.Id);
        }

        [Fact]
        public async Task DeleteServiceCategory_ServiceCategoryDoesntExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Category with entered Id does not exsist";
            _repositoryManagerMock.Setup(s => s.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), false))
                .ReturnsAsync((ServiceCategoryEntity)null);

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.DeleteServiceCategory(Guid.NewGuid());

            // Assert
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task DeleteServiceCategory_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesCategoriesRepository.GetServiceCategory(It.IsAny<Guid>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesCategoriesService.DeleteServiceCategory(Guid.NewGuid());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }
    }
}