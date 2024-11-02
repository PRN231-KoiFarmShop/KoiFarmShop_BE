using ks.application.Models.Orders;
using ks.application.Services.Interfaces;
using ks.application.Utilities;
using ks.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Services
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OrderViewModel> CreateOrder(OrderCreateModel model, CancellationToken cancellationToken = default)
        {
            var order=unitOfWork.Mapper.Map<Order>(model);
            if(order != null)
            {
                var result= await unitOfWork.OrderRepository.CreateAsync(order, cancellationToken);
                if(await unitOfWork.SaveChangesAsync())
                {
                    return unitOfWork.Mapper
                        .Map<OrderViewModel>(await unitOfWork.OrderRepository.FirstOrDefaultAsync(o=>o.Id==result, cancellationToken));
                }
                else
                {
                    throw new InvalidOperationException("Save Changes Fail");
                }
            }
            return null;
        }

        public async Task<OrderViewModel> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return unitOfWork.Mapper.Map<OrderViewModel>(await unitOfWork
                .OrderRepository.FirstOrDefaultAsync(o => o.Id == id, cancellationToken));
        }

        public async Task<PaginatedList<OrderViewModel>> GetOrders(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default)
        {
            var list = !string.IsNullOrEmpty(search)
                ? await unitOfWork.OrderRepository.WhereAsync(o => o.ShippingAddress.Contains(search, StringComparison.InvariantCultureIgnoreCase), cancellationToken)
                :await unitOfWork.OrderRepository.GetAllAsync(cancellationToken);
            if (list.Any())
            {
                return PaginatedList<OrderViewModel>.Create(unitOfWork
                    .Mapper.Map<List<OrderViewModel>>(list).AsQueryable(),
                    pageIndex, pageSize??list.Count);
            }
            else
            {
                return null;
            }
        }
        public async Task<List<OrderViewModel>> GetByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            var list = await unitOfWork.OrderRepository.WhereAsync(o => o.UserId == userId, cancellationToken);
            return unitOfWork.Mapper.Map<List<OrderViewModel>>(list);
        }

        public async Task<bool> RemoveOrder(Guid id, CancellationToken cancellationToken = default)
        {
            var exist = await unitOfWork.OrderRepository.FirstOrDefaultAsync(o => o.Id == id);
            if (exist != null)
            {
                unitOfWork.OrderRepository.SoftRemove(exist);
                return await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Order Not found?");
            }
        }

        public async Task<bool> UpdateOrder(Guid id, OrderUpdateModel model, CancellationToken cancellationToken = default)
        {
            var exist = await unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (exist != null)
            {
                unitOfWork.Mapper.Map(model, exist);
                unitOfWork.OrderRepository.Update(exist);
                return await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Order Not found?");
            }
        }
    }
}
