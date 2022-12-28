using FluentValidation;
using ModelsToRequest.RequestEntity;

namespace ServicesManager.Presentation.Validators
{
    public class ServiceCategoryForRequestValidator : AbstractValidator<ServiceCategoryRequest>
    {
        public ServiceCategoryForRequestValidator()
        {
            RuleFor(service => service.Name).NotNull().NotEmpty();
            RuleFor(service => service.TimeSlotSizeInMinutes).NotNull().NotEmpty();
        }
    }
}
