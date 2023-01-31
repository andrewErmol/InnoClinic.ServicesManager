using InnoClinic.Domain.Messages.Service;
using MassTransit;
using ServicesManager.Contracts.Models;
using ServicesManager.Services.Abstractions.IServices;

namespace ServicesManager.Services.Services
{
    public class PublishService : IPublishService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishServiceUpdatedMessage(Service service)
        {
            await _publishEndpoint.Publish(new ServiceUpdated
            {
                Id = service.Id,
                Name = service.Name
            });
        }
    }
}
