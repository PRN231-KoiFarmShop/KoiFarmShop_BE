using ks.application.Models.VNPays;

namespace ks.application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> RequestVNPayCommand(Guid orderId);
        Task<string> RequestZaloPay(Guid orderId);
        Task<bool> CreatePayment(VNPayResponseModel model);
    }
}
