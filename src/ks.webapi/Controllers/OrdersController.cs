using ks.application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IPaymentService paymentService;
    public OrdersController(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }
    [HttpPost("{id}/vnpay")]
    public async Task<IActionResult> CreatePaymentRequest([FromRoute] Guid id)
    {
        var result = await paymentService.RequestVNPayCommand(id);
        return Ok(result);
    }

}