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
    public class ServicesServiceTests
    {
        private readonly Mock<IRepositoryManager> _repositoryManagerMock;
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ServicesServiceTests()
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

        private List<ServiceEntity> _testServices = new List<ServiceEntity>
        {
            new ServiceEntity
            {
                Id = new Guid("3e77e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Name = "Therapi",
                CategoryId = new Guid("7e33e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Price = 60,
                SpecializationId = new Guid("5e55e8aa-b920-4bca-adb6-4a5e2de117f2"),
            },
            new ServiceEntity
            {
                Id = new Guid("e3d934ae-4002-41b7-8dcf-99709425f227"),
                Name = "String",
                CategoryId = new Guid("8e33e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Price = 60,
                SpecializationId = new Guid("8e55e8aa-b920-4bca-adb6-4a5e2de117f2"),

            },
        };

        [Fact]
        public async Task GetServices_ServicesExist_ReturnsListOfServices()
        {
            // Arrange
            List<ServiceEntity> services = new List<ServiceEntity>();
            services.Add(_testServices[0]);
            services.Add(_testServices[1]);

            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetServices(false))
                .ReturnsAsync(services);

            // Act
            var result = await _serviceManager.ServicesService.GetServices();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveSameCount(services);
            result.Should().BeEquivalentTo(services, opt => opt.Excluding(x => x.Category));
        }

        [Fact]
        public async Task GetServices_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetServices(false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesService.GetServices();

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task GetServiceById_ServiceExists_ReturnsService()
        {
            // Arrange
            ServiceEntity service = _testServices[0];

            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), false))
                .ReturnsAsync(service);

            // Act
            var result = await _serviceManager.ServicesService.GetServiceById(service.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(service, opt => opt.Excluding(x => x.Category));
        }

        [Fact]
        public async Task GetServiceById_ServiceDoesNotExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Service with entered Id does not exsist";
            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), false))
                .ReturnsAsync((ServiceEntity)null);

            // Act
            Task Act() => _serviceManager.ServicesService.GetServiceById(Guid.NewGuid());

            // Assert
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task GetServiceBiId_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesService.GetServiceById(Guid.NewGuid());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task CreateService_ServiceValid_ReturnsServicesId()
        {
            // Arrange
            Service service = new Service
            {
                Name = "service",
                CategoryId = new Guid("8e77e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Price = 60,
                SpecializationId = new Guid("8e75e8aa-b920-4bca-adb6-4a5e2de117f2"),
            };

            _repositoryManagerMock.Setup(r => r.ServicesRepository.CreateService(It.IsAny<ServiceEntity>()))
                .Callback((ServiceEntity serviceEntity) =>
                {
                    serviceEntity.Id = Guid.NewGuid();
                });

            // Act
            var result = await _serviceManager.ServicesService.CreateService(service);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().NotBe(Guid.Empty);
        }

        [Fact]
        public async Task CreateService_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesRepository.CreateService(It.IsAny<ServiceEntity>()))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesService.CreateService(new Service());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task UpdateService_ServiceValid_UpdateService()
        {
            // Arrange
            ServiceEntity serviceEntity = _testServices[0];
            Service service = new Service
            {
                Name = "service",
                CategoryId = new Guid("8e77e8aa-b920-4bca-adb6-4a5e2de117f2"),
                Price = 60,
                SpecializationId = new Guid("8e75e8aa-b920-4bca-adb6-4a5e2de117f2"),
            };

            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), true))
                .ReturnsAsync(serviceEntity);
            _repositoryManagerMock.Setup(r => r.ServicesRepository.UpdateService(It.IsAny<ServiceEntity>()));

            // Act
            await _serviceManager.ServicesService.UpdateService(service.Id, service);
        }

        [Fact]
        public async Task UpdateService_ServiceDoesNotExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Service with entered Id does not exsist";

            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), true))
                .ReturnsAsync((ServiceEntity)null);

            // Act
            Task Act() => _serviceManager.ServicesService.UpdateService(Guid.NewGuid(), new Service());

            // Assert
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task UpdateService_DatabaseError_ThrowsException()
        {
            // Arrange
            ServiceEntity service = _testServices[0];

            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), true))
                .ReturnsAsync(service);
            _repositoryManagerMock.Setup(r => r.ServicesRepository.UpdateService(It.IsAny<ServiceEntity>()))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesService.UpdateService(Guid.NewGuid(), new Service());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }

        [Fact]
        public async Task DeleteService_ServiceExists_DeleteService()
        {
            // Arrange
            ServiceEntity service = _testServices[0];

            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), false))
                .ReturnsAsync(service);
            _repositoryManagerMock.Setup(r => r.ServicesRepository.DeleteService(It.IsAny<ServiceEntity>()));

            // Act
            await _serviceManager.ServicesService.DeleteService(service.Id);
        }

        [Fact]
        public async Task DeleteService_ServicesDoesntExist_ThrowsException()
        {
            // Arrange
            string errorMessage = "Service with entered Id does not exsist";
            _repositoryManagerMock.Setup(s => s.ServicesRepository.GetService(It.IsAny<Guid>(), false))
                .ReturnsAsync((ServiceEntity)null);

            // Act
            Task Act() => _serviceManager.ServicesService.DeleteService(Guid.NewGuid());

            // Assert
            var result = await Assert.ThrowsAsync<NotFoundException>(Act);
            result.Message.Should().Be(errorMessage);
        }

        [Fact]
        public async Task DeleteService_DatabaseError_ThrowsException()
        {
            // Arrange
            _repositoryManagerMock.Setup(r => r.ServicesRepository.GetService(It.IsAny<Guid>(), false))
                .ThrowsAsync(new Exception());

            // Act
            Task Act() => _serviceManager.ServicesService.DeleteService(Guid.NewGuid());

            // Assert
            await Assert.ThrowsAsync<Exception>(Act);
        }
    }
}