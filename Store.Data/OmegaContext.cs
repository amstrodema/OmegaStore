using Microsoft.EntityFrameworkCore;
using Store.Model;

namespace Store.Data
{
    public class OmegaContext : DbContext
    {
        public OmegaContext(DbContextOptions<OmegaContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(o => o.Amount).HasPrecision(14, 2);
            modelBuilder.Entity<Item>().Property(o => o.Price).HasPrecision(14, 2);
            modelBuilder.Entity<Item>().Property(o => o.OldPrice).HasPrecision(14, 2);
        }
        public virtual DbSet<BillingDetail> BillingDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<FeatureOption> FeatureOptions { get; set; }
        public virtual DbSet<Model.File> Files { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemFeature> ItemFeatures { get; set; }
        public virtual DbSet<Key> Keys { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Tracking> Trackings { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }
        public virtual DbSet<Model.Store> Stores { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<LoginMonitor> LoginMonitors { get; set; }
    }
}