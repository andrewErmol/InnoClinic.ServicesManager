namespace ServicesManager.Domain.Entities
{
    public class ServiceCategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TimeSlotSizeInMinutes { get; set; }
    }
}
