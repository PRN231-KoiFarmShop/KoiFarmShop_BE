using ks.application.Models.VNPays;
using ks.application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ks.webapi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService paymentService;
    public PaymentsController(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }


    [HttpGet("vn-pay/response")]
    public async Task<IActionResult> CreatePayment([FromQuery] VNPayResponseModel model)
    {
        string html = string.Empty;
        var result = await paymentService.CreatePayment(model);

        if (result)
        {
            html = System.IO.File.ReadAllText(@"./wwwroot/payment-success.html")
                .Replace("{{Amount}}", (model.vnp_Amount / 100).ToString("#,##0"))
                .Replace("{{CreateDate}}", DateTime.Now.ToString("dd/MM/yyyy"));
            return base.Content(html, "text/html");

        }
        else
        {
            html = System.IO.File.ReadAllText(@"./wwwroot/payment-fail.html");
            return base.Content(html, "text/html");
        }
 
    }


}
