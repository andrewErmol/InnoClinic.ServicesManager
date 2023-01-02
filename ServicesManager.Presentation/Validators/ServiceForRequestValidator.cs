using FluentValidation;
using ModelsToRequest.RequestEntity;

namespace ServicesManager.Presentation.Validators
{
    public class ServiceForRequestValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceForRequestValidator()
        {
            RuleFor(service => service.Name).NotNull().NotEmpty();
            RuleFor(service => service.CategoryId).NotNull().NotEmpty();
            RuleFor(service => service.Price).NotNull().NotEmpty();
            RuleFor(service => service.SpecializationId).NotNull().NotEmpty();
        }
    }
}
