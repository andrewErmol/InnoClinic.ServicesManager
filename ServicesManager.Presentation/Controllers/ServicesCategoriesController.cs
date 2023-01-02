using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsToRequest.RequestEntity;
using ServicesManager.Contracts.Models;
using ServicesManager.Services.Abstractions.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace ServicesCategoriesManager.Presentation.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class ServicesCategoriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ServicesCategoriesController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of categories",
            Description = "This endpoint allows to get a list categories.")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of categories", typeof(IEnumerable<ServiceCategory>))]
        public async Task<IActionResult> GetServicesCategories()
        {
            return Ok(await _serviceManager.ServicesCategoriesService.GetServicesCategories());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a Category",
            Description = "This endpoint allows to get a category.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Category", typeof(ServiceCategory))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity with entered id doesn't exist")]
        public async Task<IActionResult> GetServiceCategory(Guid id)
        {
            var serviceCategory = await _serviceManager.ServicesCategoriesService.GetServiceCategoryById(id);

            return Ok(serviceCategory);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a category",
            Description = "This endpoint allows to create a category.")]
        [SwaggerResponse(StatusCodes.Status201Created, "The created Category Id", typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        public async Task<IActionResult> CreateServiceCategory([FromBody] ServiceCategoryRequest serviceCategoryForRequest)
        {
            var serviceCategory = _mapper.Map<ServiceCategory>(serviceCategoryForRequest);

            var createdServiceCategoryId = await _serviceManager.ServicesCategoriesService.CreateServiceCategory(serviceCategory);

            return CreatedAtAction(nameof(GetServicesCategories), new { id = createdServiceCategoryId });
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a category",
            Description = "This endpoint allows to update a category.")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity with entered id doesn't exist")]
        public async Task<IActionResult> UpdateServiceCategory(Guid id, [FromBody] ServiceCategoryRequest serviceCategoryForRequest)
        {
            var serviceCategory = _mapper.Map<ServiceCategory>(serviceCategoryForRequest);

            await _serviceManager.ServicesCategoriesService.UpdateServiceCategory(id, serviceCategory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a category",
            Description = "This endpoint allows to delete a category.")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity with entered id doesn't exist")]
        public async Task<IActionResult> DeleteServiceCategory(Guid id)
        {
            await _serviceManager.ServicesCategoriesService.DeleteServiceCategory(id);

            return NoContent();
        }
    }
}