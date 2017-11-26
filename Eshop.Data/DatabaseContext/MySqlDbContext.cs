using Eshop.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Eshop.Data.DatabaseContext
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext() : base("Default")
        {
            //Here we can add some data to db when we initialize db, for now we do not need it
            //Database.SetInitializer<MySqlDbContext>(new MySqlDbInitializer());
            Database.SetInitializer<MySqlDbContext>(new MySqlDbInitializer());
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountInfo> AccountInfos { get; set; }
        public DbSet<Entities.Attribute> Attributes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartInfo> CartInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductPic> ProductPics { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseInfo> PurchaseInfos { get; set; }
        public DbSet<TraitValue> TraitValues { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<WishListProduct> WishListProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
    
}
