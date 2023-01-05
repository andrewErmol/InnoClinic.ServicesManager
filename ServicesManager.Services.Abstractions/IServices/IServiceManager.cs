namespace ServicesManager.Services.Abstractions.IServices
{
    public interface IServiceManager
    {
        IServicesService ServicesService { get; }
        IServicesCategoriesService ServicesCategoriesService { get; }
    }
}
