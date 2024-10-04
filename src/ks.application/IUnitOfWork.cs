using AutoMapper;
using ks.application.Repositories;

namespace ks.application;
public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderLineRepository OrderLineRepository { get; }
    IFishPackageRepository FishPackageRepository { get; }
    IFishRepository FishRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IFeedbackRepository FeedbackRepository { get; }
    INewsRepository NewsRepository { get; }
    IMapper Mapper { get; }
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task SeedData();


}