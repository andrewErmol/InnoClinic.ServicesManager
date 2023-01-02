using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsToRequest.RequestEntity;
using ServicesManager.Contracts.Models;
using ServicesManager.Services.Abstractions.IServices;
using Swashbuckle.AspNetCore.Annotations;

namespace ServicesManager.Presentation.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly IMapper _mapper;

        public ServicesController(IServiceManager serviceManager, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get a list of services",
            Description = "This endpoint allows to get a list services.")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of services", typeof(IEnumerable<Service>))]
        public async Task<IActionResult> GetServices()
        {
            return Ok(await _serviceManager.ServicesService.GetServices());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a service",
            Description = "This endpoint allows to get a service.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Service", typeof(Service))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity with entered id doesn't exist")]
        public async Task<IActionResult> GetService(Guid id)
        {
            var service = await _serviceManager.ServicesService.GetServiceById(id);

            return Ok(service);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a service",
            Description = "This endpoint allows to create a service.")]
        [SwaggerResponse(StatusCodes.Status201Created, "The created services Id", typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest serviceForRequest)
        {
            var service = _mapper.Map<Service>(serviceForRequest);

            var createdServiceId = await _serviceManager.ServicesService.CreateService(service);

            return CreatedAtAction(nameof(GetServices), new { id = createdServiceId });
        }
                
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a service",
            Description = "This endpoint allows to update a service.")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity with entered id doesn't exist")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceRequest serviceForRequest)
        {
            var service = _mapper.Map<Service>(serviceForRequest);

            await _serviceManager.ServicesService.UpdateService(id, service);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a service",
            Description = "This endpoint allows to delete a service.")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Entity with entered id doesn't exist")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            await _serviceManager.ServicesService.DeleteService(id);

            return NoContent();
        }
    }
}