using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesManager.Services.Abstractions.IServices
{
    public interface IServiceManager
    {
        IServicesService ServicesService { get; }
        IServicesCategoriesService ServicesCategoriesService { get; }
    }
}
