namespace ServicesManager.Domain.Entities
{
    public class ServiceEntity
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public Guid SpecializationId { get; set; }
        public decimal Price { get; set; }
        public virtual ServiceCategoryEntity Category { get; set; }
    }
}
