using System.Data.Entity;

namespace MaiTrinhWeb.Data
{
    public class MaiTrinhWebContext : DbContext
    {
        public MaiTrinhWebContext() : base("Name=MaiTrinhWebConnectionString")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<SellProduct> SellProducts { get; set; }
        public DbSet<ImportProduct> ImportProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Color> Colors { get; set; }
    }
}
