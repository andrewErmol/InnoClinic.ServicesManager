namespace ServicesManager.Domain.IRepositories
{
    public interface IRepositoryManager
    {
        IServicesRepository ServicesRepository { get; }
        IServicesCategoriesRepository ServicesCategoriesRepository { get; }
    }
}
