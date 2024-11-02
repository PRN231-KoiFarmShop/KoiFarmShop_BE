using AutoMapper;
using ks.application.Models.Feedbacks;
using ks.application.Models.Fish;
using ks.application.Models.FishPackages;
using ks.application.Models.News;
using ks.application.Models.OrderLines;
using ks.application.Models.Orders;
using ks.application.Models.Users;
using ks.domain.Entities;

namespace ks.application.MapperProfiles;
public class MapperConfigurationProfile : Profile
{
    public MapperConfigurationProfile()
    {
        #region  UserMapper
        CreateMap<UserViewModel, User>()
            .ReverseMap()
            .ForMember(x => x.Address, cfg => cfg.MapFrom(x => string.Join(',', x.Address)));
        CreateMap<UserCreateModel, User>()
         .ForMember(x => x.Address, cfg => cfg.MapFrom(x => new List<string>()
            {
                x.Address!
            }))
            .ReverseMap();
        CreateMap<UserUpdateModel, User>()
         .ForMember(x => x.Address, cfg => cfg.MapFrom(x => new List<string>()
            {
                x.Address!
            }))
            .ReverseMap();
        #endregion

        #region FishMapper
        CreateMap<FishViewModel, Fish>()
            .ReverseMap();
        CreateMap<FishCreateModel, Fish>()
            .ReverseMap();
        CreateMap<FishUpdateModel, Fish>()
            .ReverseMap();
        #endregion

        #region  FishPack
        CreateMap<FishPackage, FishPackageViewModel>()
            .ReverseMap()
            .ForMember(x => x.Fishes, cfg => cfg.MapFrom(x => x.Fishes));
        CreateMap<FishPackageUpdateModel, FishPackage>()
            .ReverseMap();
        #endregion

        #region NewsMapper
        CreateMap<News, NewsViewModel>()
            .ReverseMap();
        CreateMap<News, CreateNewsModel>()
            .ReverseMap();
        CreateMap<News, NewsUpdateModel>()
            .ReverseMap();
        #endregion

        #region Order
        CreateMap<OrderViewModel, Order>()
                .ReverseMap();
        CreateMap<Order, OrderCreateModel>()
            .ReverseMap();
        CreateMap<Order, OrderUpdateModel>().ReverseMap();
        #endregion

        #region OrderLines
        CreateMap<OrderLine, OrderLineFishCreateModel>()
            .ReverseMap();
        CreateMap<OrderLine, OrderLinePackageCreateModel>()
            .ReverseMap();
        CreateMap<OrderLine, OrderLineViewModel>().
            ReverseMap();
        #endregion

        #region Feedback
        CreateMap<FeedbackViewModel, Feedback>()
            .ForMember(x => x.Order, cfg => cfg.MapFrom(x => x.Order))
            .ReverseMap();

        #endregion
    }
}