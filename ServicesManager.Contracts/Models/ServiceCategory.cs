namespace ServicesManager.Contracts.Models
{
    public class ServiceCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TimeSlotSizeInMinutes { get; set; }
    }
}
