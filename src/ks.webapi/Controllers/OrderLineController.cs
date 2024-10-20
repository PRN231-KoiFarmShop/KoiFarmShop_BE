using ks.application.Models.OrderLines;
using ks.application.Models.Orders;
using ks.application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : ControllerBase
    {
        private readonly IOrderLineService _service;
        public OrderLineController(IOrderLineService service)
        {
            _service = service;
        }
        [HttpGet("OrderId/{id}")]
        public async Task<IActionResult> GetByOrderId([FromRoute] Guid id)
        {
            var result= await _service.GetByOrderId(id);
            return result is not null
            ? Ok(result)
            : throw new Exception($"Not found Id: {id}");
        }
        [HttpPost("FishLine")]
        public async Task<IActionResult> CreateFish([FromBody] OrderLineFishCreateModel model)
        {
            var result=await _service.CreateFish(model,default);
            return result is not null
            ? Ok(result)
            : throw new Exception("Created Failed");
        }
        [HttpPost("PackageLine")]
        public async Task<IActionResult> CreatePackage([FromBody] OrderLinePackageCreateModel model)
        {
            var result = await _service.CreatePackage(model, default);
            return result is not null
            ? Ok(result)
            : throw new Exception("Created Failed");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete([FromRoute] Guid id)
        {
            var result=await _service.Delete(id);
            return result
            ? NoContent()
            : throw new Exception("Delete Failed");
        }
    }
}
