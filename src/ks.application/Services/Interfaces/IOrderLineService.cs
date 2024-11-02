using ks.application.Models.OrderLines;
using ks.application.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Services.Interfaces
{
    public interface IOrderLineService
    {
        Task<List<OrderLineViewModel>> GetByOrderId(Guid orderId, CancellationToken cancellationToken = default);
        Task<OrderLineViewModel> CreateFish(OrderLineFishCreateModel model, CancellationToken cancellationToken = default);
        Task<OrderLineViewModel> CreatePackage(OrderLinePackageCreateModel model, CancellationToken cancellationToken = default);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
    }
}
