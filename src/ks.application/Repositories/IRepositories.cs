using ks.domain.Entities;

namespace ks.application.Repositories;
public interface IUserRepository : IGenericRepository<User>;
public interface IFishRepository : IGenericRepository<Fish>;
public interface IFishPackageRepository : IGenericRepository<FishPackage>;
public interface IOrderRepository : IGenericRepository<Order>;
public interface IOrderLineRepository : IGenericRepository<OrderLine>;
public interface IPaymentRepository : IGenericRepository<Payment>;
public interface IFeedbackRepository : IGenericRepository<Feedback>;
public interface INewsRepository : IGenericRepository<News>;