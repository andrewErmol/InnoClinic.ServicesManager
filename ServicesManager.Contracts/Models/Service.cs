namespace ServicesManager.Contracts.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid SpecializationId { get; set; }
        public string CategoryName { get; set; }
    }
}
