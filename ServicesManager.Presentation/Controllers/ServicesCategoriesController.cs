using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelsToRequest.RequestEntity;
using ServicesManager.Contracts.Models;
using ServicesManager.Services.Abstractions.IServices;

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
        public async Task<IActionResult> GetServicesCategories()
        {
            return Ok(await _serviceManager.ServicesCategoriesService.GetServicesCategories());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceCategory(Guid id)
        {
            var serviceCategory = await _serviceManager.ServicesCategoriesService.GetServiceCategoryById(id);

            return Ok(serviceCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceCategory([FromBody] ServiceCategoryRequest serviceCategoryForRequest)
        {
            var serviceCategory = _mapper.Map<ServiceCategory>(serviceCategoryForRequest);

            var createdServiceCategoryId = await _serviceManager.ServicesCategoriesService.CreateServiceCategory(serviceCategory);

            return CreatedAtAction(nameof(GetServicesCategories), new { id = createdServiceCategoryId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceCategory(Guid id, [FromBody] ServiceCategoryRequest serviceCategoryForRequest)
        {
            var serviceCategory = _mapper.Map<ServiceCategory>(serviceCategoryForRequest);

            await _serviceManager.ServicesCategoriesService.UpdateServiceCategory(id, serviceCategory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceCategory(Guid id)
        {
            await _serviceManager.ServicesCategoriesService.DeleteServiceCategory(id);

            return NoContent();
        }
    }
}