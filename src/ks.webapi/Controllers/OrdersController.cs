using ks.application.Models.Orders;
using ks.application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers;
[ApiController]
[Route("api/[controller]")]

public class OrdersController : ControllerBase
{
    private readonly IPaymentService paymentService;
    private readonly IOrderService orderService;
    public OrdersController(IPaymentService paymentService, IOrderService orderService)
    {
        this.paymentService = paymentService;
        this.orderService = orderService;
    }
    [HttpPost("{id}/vnpay")]
    public async Task<IActionResult> CreatePaymentRequest([FromRoute] Guid id)
    {
        var result = await paymentService.RequestVNPayCommand(id);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? pageSize,
        [FromQuery] string search = "",
        [FromQuery] int pageIndex = 0)
    {
        var result = await orderService.GetOrders(pageSize: pageSize, search: search, pageIndex: pageIndex, cancellationToken: default);
        return result.Count > 0
            ? Ok(result) : throw new Exception("List is empty");
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await orderService.GetById(id, cancellationToken: default);
        return result is not null
            ? Ok(result)
            : throw new Exception($"Not found Id: {id}");
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateModel model)
    {
        var result = await orderService.CreateOrder(model, default);
        return result is not null
            ? Ok(result)
            : throw new Exception("Created Failed");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult>DeleteOrder([FromRoute]Guid id)
    {
        var result=await orderService.RemoveOrder(id, cancellationToken: default);
        return result
            ? NoContent()
            : throw new Exception("Delete Failed");
    }
    [HttpPut("{id}")]
    
    public async Task<IActionResult> UpdateOrder([FromRoute]Guid id, [FromBody] OrderUpdateModel model)
    {
        var result=await orderService.UpdateOrder(id, model, default);
        return result
            ? NoContent()
            : throw new Exception("Update Failed");
    }
}