using ks.application.Models.Fish;
using ks.application.Models.Orders;
using ks.application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PaginatedList<OrderViewModel>> GetOrders(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default);
        Task<OrderViewModel> GetById(Guid id, CancellationToken cancellationToken=default);
        Task<List<OrderViewModel>> GetByUserId(Guid userId, CancellationToken cancellationToken = default);
        Task<OrderViewModel> CreateOrder(OrderCreateModel model, CancellationToken cancellationToken=default);
        Task<bool> UpdateOrder(Guid id, OrderUpdateModel model, CancellationToken cancellationToken=default);
        Task<bool> RemoveOrder(Guid id, CancellationToken cancellationToken = default);
    }
}
