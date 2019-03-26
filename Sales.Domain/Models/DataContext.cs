namespace Sales.Domain.Models
{
    using Sales.Common.Models;
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")    // forma de conectar a la bse de datos
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
