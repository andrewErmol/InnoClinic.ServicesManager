using ServicesManager.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesManager.Services.Abstractions.IServices
{
    public interface IPublishService
    {
        Task PublishServiceUpdatedMessage(Service service);
    }
}
