
using System.Runtime.CompilerServices;
using ks.application.Models.VNPays;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using ks.application.Utilities.Zalopay;
using ks.domain.Entities;
using ks.domain.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ks.application.Services;
public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IConfiguration configuration;
    public PaymentService(IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        this.configuration = configuration;
        this.unitOfWork = unitOfWork;
    }
    public async Task<bool> CreatePayment(VNPayResponseModel model)
    {
        if (model.vnp_TransactionStatus != "00")
        {
            return false;
        }

        var isExisted = await unitOfWork.PaymentRepository.FirstOrDefaultAsync(x => x.TransactionNo == model.vnp_TransactionNo
            && x.TxnRef == model.vnp_TxnRef);
        if (isExisted is not null)
        {
            throw new Exception("Đã thanh toán thành công! Không thể thanh toán lại");
        }
        var order = await unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id == model.orderId)
            ?? throw new Exception($"Not found Order with id: {model.orderId}");
        order.UserId = order.User.Id;
        var payment = new Payment()
        {
            Amount = order.ActualAmount,
            Order = order,
            OrderId = order.Id,
            TxnRef = model.vnp_TxnRef,
            TransactionNo = model.vnp_TransactionNo,
            PaymentType = PaymentMethodEnum.VNPay.ToString(),
            IsCompleted = true,
        };
        order.IsSuccess = true;
        unitOfWork.OrderRepository.Update(order);
        await unitOfWork.PaymentRepository.CreateAsync(payment);
        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<string> RequestVNPayCommand(Guid orderId)
    {
        var order = await unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id == orderId);
        if (order is not null)
        {
            VNPayRequestModel payRequest = new();
            VnPayLibrary vnPayLib = new();
            vnPayLib.AddRequestData("vnp_Version", payRequest.Version);
            vnPayLib.AddRequestData("vnp_Command", payRequest.Command);
            vnPayLib.AddRequestData("vnp_TmnCode", configuration["VNPay:TmnCode"] ?? string.Empty);
            vnPayLib.AddRequestData("vnp_Amount", ((int)order.TotalAmount * 100).ToString());
            vnPayLib.AddRequestData("vnp_CreateDate", payRequest.CreateDate);
            vnPayLib.AddRequestData("vnp_CurrCode", payRequest.CurrCode);
            vnPayLib.AddRequestData("vnp_IpAddr", payRequest.IpAddress);
            vnPayLib.AddRequestData("vnp_Locale", payRequest.Locale);
            vnPayLib.AddRequestData("vnp_OrderInfo", $"Thanh Toán Đơn Hàng {order.Id}");
            vnPayLib.AddRequestData("vnp_OrderType", payRequest.OrderType);
            vnPayLib.AddRequestData("vnp_ReturnUrl", $"http://localhost:5000/api/payments/vn-pay/response?orderId={orderId}");
            vnPayLib.AddRequestData("vnp_TxnRef", payRequest.TxnRef);
            var paymentUrl = vnPayLib.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
              configuration["VNPay:HashSecret"] ?? string.Empty);
            return paymentUrl;
        }
        else
        {
            throw new Exception($"Not found Order with Id {orderId}");
        }
    }

    public async Task<string> RequestZaloPay(Guid orderId)
    {
        var order = await unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id == orderId);
        if (order is not null)
        {
            Random rnd = new Random();
            var embed_data = new {
                redirect_url = "http://localhost:5000"
             };
            var items = new[]{new{}};
           
            var app_trans_id   = rnd.Next(1000000); // Generate a random order's ID.
            var param = new Dictionary<string, string>
            {
                { "app_id", "554" },
                { "app_user", "user123" },
                { "app_time", ZalopayUtils.GetTimeStamp().ToString() },
                { "amount", order.ActualAmount.ToString() },
                { "app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id }, // mã giao dich có định dạng yyMMdd_xxxx
                { "embed_data", JsonConvert.SerializeObject(embed_data) },
                { "item", JsonConvert.SerializeObject(items) },
                { "description", "Lazada - Thanh toán đơn hàng #" + app_trans_id },
                { "bank_code", "zalopayapp" }
            };    

            var data = 554 + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|" 
                + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, configuration["Zalopay:Key1"] ?? string.Empty, data));

            var result = await ZalopayHttpHelper.PostFormAsync("https://sb-openapi.zalopay.vn/v2/create", param);
          

            var param2 = new Dictionary<string, string>();
            param2.Add("appid", "554");
            param2.Add("apptransid", "424172");
            var data2 = 554 + "|" + "424172" + "|" + configuration["Zalopay:Key1"]; 

            param2.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, configuration["Zalopay:Key1"] ?? string.Empty, data2));

            var result2 = await ZalopayHttpHelper.PostFormAsync("https://sandbox.zalopay.com.vn/v001/tpe/getstatusbyapptransid", param2);

            foreach(var entry in result) {
                Console.WriteLine("{0} = {1}", entry.Key, entry.Value);
            }
            return string.Empty;
        }
        else
        {
            throw new Exception($"Not found Order with Id {orderId}");
        }
    }
}