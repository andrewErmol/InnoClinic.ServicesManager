namespace ModelsToRequest.RequestEntity
{
    public class ServiceRequest
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid SpecializationId { get; set; }
    }
}
