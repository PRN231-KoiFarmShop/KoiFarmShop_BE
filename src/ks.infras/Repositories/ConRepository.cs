using ks.application.Repositories;
using ks.domain.Entities;

namespace ks.infras.Repositories;
public class UserRepository(IServiceProvider serviceProvider)
    : GenericRepository<User>(serviceProvider), IUserRepository;
public class OrderRepository(IServiceProvider serviceProvider)
    : GenericRepository<Order>(serviceProvider), IOrderRepository;
public class FishRepository(IServiceProvider serviceProvider)
    : GenericRepository<Fish>(serviceProvider), IFishRepository;
public class FishPackageRepository(IServiceProvider serviceProvider)
    : GenericRepository<FishPackage>(serviceProvider), IFishPackageRepository;
public class PaymentRepository(IServiceProvider serviceProvider)
    : GenericRepository<Payment>(serviceProvider), IPaymentRepository;
public class OrderLineRepository(IServiceProvider serviceProvider)
    : GenericRepository<OrderLine>(serviceProvider), IOrderLineRepository;
public class FeedbackRepository(IServiceProvider serviceProvider)
    : GenericRepository<Feedback>(serviceProvider), IFeedbackRepository;
public class NewsRepository(IServiceProvider serviceProvider)
    : GenericRepository<News>(serviceProvider), INewsRepository;