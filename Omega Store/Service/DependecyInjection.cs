using Store.Business;
using Store.Data;
using Store.Data.Interface;
using Store.Data.Repository;

namespace Omega_Store.Service
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
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IUser, UserRepository>()
                .AddTransient<IOrder, OrderRepository>()
                .AddTransient<ICategory, CategoryRepository>()
                .AddTransient<IBillingDetail, BillingDetailRepository>()
                .AddTransient<ICustomer, CustomerRepository>()
                .AddTransient<IFeature, FeatureRepository>()
                .AddTransient<IFeatureOption, FeatureOptionRepository>()
                .AddTransient<IFile, FileRepository>()
                .AddTransient<IItem, ItemRepository>()
                .AddTransient<IItemFeature, ItemFeatureRepository>()
                .AddTransient<IKey, KeyRepository>()
                .AddTransient<INotification, NotificationRepository>()
                .AddTransient<IOrderItem, OrderItemRepository>()
                .AddTransient<IReview, ReviewRepository>()
                .AddTransient<IShippingDetail, ShippingDetailRepository>()
                .AddTransient<IStore, StoreRepository>()
                .AddTransient<ITracking, TrackingRepository>()
                .AddTransient<IPayment, PaymentRepository>()
                .AddTransient<ITransaction, TransactionRepository>()
                .AddTransient<ILoginMonitor, LoginMonitorRepository>()

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
