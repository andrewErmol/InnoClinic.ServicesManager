using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesManager.Domain.IRepositories
{
    public interface IRepositoryManager
    {
        IServicesRepository ServicesRepository { get; }
        IServicesCategoriesRepository ServicesCategoriesRepository { get; }
    }
}
