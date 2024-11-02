using ks.application.Models.OrderLines;
using ks.application.Models.Orders;
using ks.application.Services.Interfaces;
using ks.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ks.application.Services
{
    public class OrderLineService:IOrderLineService
    {
        private readonly IUnitOfWork unitOfWork;
        public OrderLineService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OrderLineViewModel> CreateFish(OrderLineFishCreateModel model, CancellationToken cancellationToken = default)
        {
            var fishExist = await unitOfWork.FishRepository.GetAllAsync(cancellationToken);
            var orderExist=await unitOfWork.OrderRepository.GetAllAsync(cancellationToken);
            var orderLine = unitOfWork.Mapper.Map<OrderLine>(model);
            if(orderLine != null&&fishExist.Any(f => f.Id == model.FishId)
                &&orderExist.Any(o=>o.Id==model.OrderId))
            {
                orderLine.FishPackageId=null;
                var result=await unitOfWork.OrderLineRepository.CreateAsync(orderLine, cancellationToken);
                if(await unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    return unitOfWork.Mapper.Map<OrderLineViewModel>
                        (await unitOfWork.OrderLineRepository.FirstOrDefaultAsync(o => o.Id == result, cancellationToken));
                }
                else
                {
                    throw new InvalidOperationException("Save Changes Fail");
                }
            }
            return null;
        }

        public async Task<OrderLineViewModel> CreatePackage(OrderLinePackageCreateModel model, CancellationToken cancellationToken = default)
        {
            var packageExist = await unitOfWork.FishPackageRepository.GetAllAsync(cancellationToken);
            var orderExist = await unitOfWork.OrderRepository.GetAllAsync(cancellationToken);
            var orderLine = unitOfWork.Mapper.Map<OrderLine>(model);
            if (orderLine != null && packageExist.Any(f => f.Id == model.FishPackageId)
                && orderExist.Any(o => o.Id == model.OrderId))
            {
                orderLine.FishId=null;
                var result = await unitOfWork.OrderLineRepository.CreateAsync(orderLine, cancellationToken);
                if (await unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    return unitOfWork.Mapper.Map<OrderLineViewModel>
                        (await unitOfWork.OrderLineRepository.FirstOrDefaultAsync(o => o.Id == result, cancellationToken));
                }
                else
                {
                    throw new InvalidOperationException("Save Changes Fail");
                }
            }
            return null;
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var exist = await unitOfWork.OrderLineRepository.FirstOrDefaultAsync(o => o.Id == id);
            if (exist != null)
            {
                unitOfWork.OrderLineRepository.SoftRemove(exist);
                return await unitOfWork.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Order Line Not found?");
            }
        }

        public async Task<List<OrderLineViewModel>> GetByOrderId(Guid orderId, CancellationToken cancellationToken = default)
        {
            return unitOfWork.Mapper.Map<List<OrderLineViewModel>>(await unitOfWork
                .OrderLineRepository.WhereAsync(o => o.OrderId == orderId, cancellationToken));
        }
    }
}
