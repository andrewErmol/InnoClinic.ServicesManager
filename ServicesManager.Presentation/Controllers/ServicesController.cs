using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModelsToRequest.RequestEntity;
using ServicesManager.Contracts.Models;
using ServicesManager.Services.Abstractions.IServices;

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
        public async Task<IActionResult> GetServices()
        {
            return Ok(await _serviceManager.ServicesService.GetServices());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(Guid id)
        {
            var service = await _serviceManager.ServicesService.GetServiceById(id);

            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] ServiceRequest serviceForRequest)
        {
            var service = _mapper.Map<Service>(serviceForRequest);

            var createdServiceId = await _serviceManager.ServicesService.CreateService(service);

            return CreatedAtAction(nameof(GetServices), new { id = createdServiceId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(Guid id, [FromBody] ServiceRequest serviceForRequest)
        {
            var service = _mapper.Map<Service>(serviceForRequest);

            await _serviceManager.ServicesService.UpdateService(id, service);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            await _serviceManager.ServicesService.DeleteService(id);

            return NoContent();
        }
    }
}