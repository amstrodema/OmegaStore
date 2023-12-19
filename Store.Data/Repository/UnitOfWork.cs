using Microsoft.EntityFrameworkCore;
using Store.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OmegaContext _db;

        public IUser Users { get; }
        public IOrder Orders { get; }
        public ICategory Categories { get; }
        public IBillingDetail BillingDetails { get; }
        public ICustomer Customers { get; }
        public IFeature Features { get; }
        public IFeatureOption FeatureOptions { get; }
        public IFile Files { get; }
        public IItem Items { get; }
        public IItemFeature ItemFeatures { get; }
        public IKey Keys { get; }
        public INotification Notifications { get; }
        public IOrderItem OrderItems { get; }
        public IReview Reviews { get; }
        public IShippingDetail ShippingDetails { get; }
        public IStore Stores { get; }
        public ITracking Trackings { get; }
        public IPayment Payments { get; }
        public ITransaction Transactions { get; }
        public ILoginMonitor LoginMonitors { get; }
        public IGroup Groups { get; }

        public UnitOfWork(OmegaContext db, IUser users, IOrder orders, ICategory categories, IBillingDetail billingDetails, ICustomer customers, IFeature features, IFeatureOption featureOptions,
            IFile files, IItem items, IItemFeature itemFeatures, IKey keys, INotification notifications, IOrderItem orderItems, IReview reviews, IShippingDetail shippingDetails, IStore stores,
            ITracking trackings, IPayment payments, ITransaction transactions, ILoginMonitor loginMonitors, IGroup groups)
        {
            _db = db;
            Users = users;
            Orders = orders;
            Categories = categories;
            BillingDetails = billingDetails;
            Customers = customers;
            Features = features;
            FeatureOptions = featureOptions;
            Files = files;
            Items = items;
            ItemFeatures = itemFeatures;
            Keys = keys;
            Notifications = notifications;
            OrderItems = orderItems;
            Reviews = reviews;
            ShippingDetails = shippingDetails;
            Stores = stores;
            Trackings = trackings;
            Payments = payments;
            Transactions = transactions;
            LoginMonitors = loginMonitors;
            Groups = groups;
        }


        public async Task<int> Commit() =>
           await _db.SaveChangesAsync();

        public void Rollback() => Dispose();

        public void Dispose() =>
            _db.DisposeAsync();
    }
}
