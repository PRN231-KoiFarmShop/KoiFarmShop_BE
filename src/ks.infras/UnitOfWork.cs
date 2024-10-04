using AutoMapper;
using ks.application;
using ks.application.Repositories;
using ks.domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ks.infras;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext dbContext;
    public UnitOfWork(IServiceProvider serviceProvider)
    {
        dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        UserRepository = serviceProvider.GetRequiredService<IUserRepository>();
        OrderRepository = serviceProvider.GetRequiredService<IOrderRepository>();
        OrderLineRepository = serviceProvider.GetRequiredService<IOrderLineRepository>();
        FishPackageRepository = serviceProvider.GetRequiredService<IFishPackageRepository>();
        FishRepository = serviceProvider.GetRequiredService<IFishRepository>();
        PaymentRepository = serviceProvider.GetRequiredService<IPaymentRepository>();
        Mapper = serviceProvider.GetRequiredService<IMapper>();
        NewsRepository = serviceProvider.GetRequiredService<INewsRepository>();
        FeedbackRepository = serviceProvider.GetRequiredService<IFeedbackRepository>();
    }
    public IFeedbackRepository FeedbackRepository { get; }
    public INewsRepository NewsRepository { get; }
    public IUserRepository UserRepository { get; }

    public IOrderRepository OrderRepository { get; }

    public IOrderLineRepository OrderLineRepository { get; }

    public IFishPackageRepository FishPackageRepository { get; }

    public IFishRepository FishRepository { get; }

    public IPaymentRepository PaymentRepository { get; }

    public IMapper Mapper { get; }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync() > 0;
    }
    public async Task SeedData()
    {
        Guid userId = Guid.Parse("362c7d47-4066-4cc2-916c-5b99266f7872");
        Guid fishId1 = Guid.Parse("362c7d47-4066-4cc2-916c-5b99266f7873");
        Guid fishId2 = Guid.Parse("362c7d47-4066-4cc2-916c-5b99266f7874");
        Guid orderId = Guid.Parse("362c7d47-4066-4cc2-916c-5b99266f7875");
        Guid orderLineId = Guid.NewGuid();
        Guid fbId = Guid.NewGuid();

        dbContext.AddRange(new Fish[]
        {
        new Fish()
        {
            ImageUrls = ["https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-75.jpg",
             "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-76.jpg" ],
            IsDeleted = false,
            Name = "Hagoromo",
            Id = fishId1,
            Price = 200000,
            Size = 120,
            Weight = 10.3,
            Type = "Sanke",
            Source = "DefaultKoiFarm",
            Sex = true,
        },
        new Fish()
        {
            ImageUrls = ["https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-75.jpg",
             "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-76.jpg" ],
            IsDeleted = false,
            Name = "Aka Matsuba",
            Id = Guid.NewGuid(),
            Price = 250000,
            Size = 50,
            Weight = 5.2,
            Type = "Sanke",
            Source = "KoiFishWorld",
            Sex = false,
        },
        new Fish()
        {
            ImageUrls = ["https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-75.jpg",
             "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-76.jpg" ],
            IsDeleted = false,
            Name = "Benigoi",
            Id = Guid.NewGuid(),
            Price = 300000,
            Size = 60,
            Weight = 6.3,
            Type = "Tancho",
            Source = "TanchoFarm",
            Sex = true,
        },
        new Fish()
        {
            ImageUrls = ["https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-75.jpg",
             "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-76.jpg" ],
            IsDeleted = false,
            Name = "Kohaku",
            Id = Guid.NewGuid(),
            Price = 350000,
            Size = 70,
            Weight = 7.1,
            Type = "Tancho",
            Source = "PremiumKoiFarm",
            Sex = false,
        },
        new Fish()
        {
            ImageUrls = ["https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-75.jpg",
             "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-76.jpg" ],
            IsDeleted = false,
            Name = "Red Asagi",
            Id = Guid.NewGuid(),
            Price = 180000,
            Size = 40,
            Weight = 4.5,
            Type = "Asagi",
            Source = "AsagiKoiCenter",
            Sex = true,
        },
        new Fish()
        {
            ImageUrls = ["https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-75.jpg",
             "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-76.jpg" ],
            IsDeleted = false,
            Name = "Shiro Utsuri",
            Id = Guid.NewGuid(),
            Price = 220000,
            Size = 65,
            Weight = 5.8,
            Type = "Utsuri",
            Source = "ShiroKoiFarm",
            Sex = false,
        }
        });
        dbContext.SaveChanges();
        dbContext.Add(new Order()
        {
            Id = orderId,
            ActualAmount = 500000,
            SalePercent = 0,
            IsDeleted = false,
            PaymentMethod = domain.Enums.PaymentMethodEnum.COD,
            ShippingAddress = "FPT",
            TotalAmount = 500000,
            TotalQuantity = 1,
            UserId = userId,
            User = new User()
            {
                HashPassword = "3zTd3+wlrHob1U5LRUAiaFRLiC2jb6sSTlG4R9qoshI=",
                Salt = new byte[]
            {
            0x48, 0x5E, 0xF7, 0xEE, 0x10, 0xC1, 0x87, 0x93,
            0x5B, 0x3C, 0xEC, 0xA1, 0x16, 0xE1, 0xE1, 0xEF
            },
                Address = ["69 Tran Duy Hung"],
                Email = "default_cus@gmail.com",
                Id = userId,
                FullName = "Default",
                PhoneNumber = "0918696969",
                Role = domain.Enums.RoleEnum.Customer,
                IsDeleted = false,
                Status = domain.Enums.UserStatusEnum.Active
            },
            OrderLines = [new OrderLine()
        {
            FishId = fishId1,
            Fish =new Fish()
        {
            ImageUrls = [ "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-54.jpg",
                "https://d2e07cbkdk0gwy.cloudfront.net/wp-content/uploads/2024/10/2024.10.01_G_080_13-55.jpg" ],
            IsDeleted = false,
            Name = "Shusui",
            Id = fishId2,
            Price = 500000,
            Size = 25.3,
            Weight = 3.5,
            Type = "Shusui",
            Source = "DefaultKoiFarm",
            Sex = true,
        },
            Id = orderLineId,
            OrderId = orderId
        }],
            Feedbacks = [new Feedback()
        {
            Id = fbId,
            Message = "Good",
            OrderId = orderId,
            Rating = 4.5,
            IsDeleted = false }
        ]
        });
        dbContext.SaveChanges();

        await Task.CompletedTask;

    }
}