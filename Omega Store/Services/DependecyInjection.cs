using Store.Business;
using Store.Data;
using Store.Data.Interface;
using Store.Data.Repository;

namespace Omega_Store.Services
{
    public class DependecyInjection
    {
        public static void Register(IServiceCollection services)
        {
            services

                .AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<GenericBusiness>()
                .AddTransient<LoginValidator>()
                .AddScoped<OmegaContext>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUser, UserRepository>()
                .AddScoped<IOrder, OrderRepository>()
                .AddScoped<ICategory, CategoryRepository>()
                .AddScoped<IBillingDetail, BillingDetailRepository>()
                .AddScoped<ICustomer, CustomerRepository>()
                .AddScoped<IFeature, FeatureRepository>()
                .AddScoped<IFeatureOption, FeatureOptionRepository>()
                .AddScoped<IFile, FileRepository>()
                .AddScoped<IItem, ItemRepository>()
                .AddScoped<IItemFeature, ItemFeatureRepository>()
                .AddScoped<IKey, KeyRepository>()
                .AddScoped<INotification, NotificationRepository>()
                .AddScoped<IOrderItem, OrderItemRepository>()
                .AddScoped<IReview, ReviewRepository>()
                .AddScoped<IShippingDetail, ShippingDetailRepository>()
                .AddScoped<IStore, StoreRepository>()
                .AddScoped<ITracking, TrackingRepository>()
                .AddScoped<IPayment, PaymentRepository>()
                .AddScoped<ITransaction, TransactionRepository>()
                .AddScoped<ILoginMonitor, LoginMonitorRepository>()
                .AddScoped<IGroup, GroupRepository>()
                .AddScoped<IFavourite, FavouriteRepository>()

                .AddScoped<GeneralBusiness>()
                .AddScoped<GroupBusiness>()
                .AddScoped<BillingDetailBusiness>()
                .AddScoped<CategoryBusiness>()
                .AddScoped<CustomerBusiness>()
                .AddScoped<FeatureBusiness>()
                .AddScoped<FeatureOptionBusiness>()
                .AddScoped<FileBusiness>()
                .AddScoped<ItemBusiness>()
                .AddScoped<ItemFeatureBusiness>()
                .AddScoped<KeyBusiness>()
                .AddScoped<NotificationBusiness>()
                .AddScoped<OrderBusiness>()
                .AddScoped<OrderItemBusiness>()
                .AddScoped<PaymentBusiness>()
                .AddScoped<ReviewBusiness>()
                .AddScoped<ShippingDetailBusiness>()
                .AddScoped<StoreBusiness>()
                .AddScoped<TrackingBusiness>()
                .AddScoped<TransactionBusiness>()
                .AddScoped<UserBusiness>()
                ;
        }

    }
}
