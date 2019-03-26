 namespace Sales.Backend.Models
{
    using Sales.Common.Models;
    using Sales.Domain.Models;
    public class LocalDataContext : DataContext
    {
         public System.Data.Entity.DbSet<Sales.Common.Models.Product> Products { get; set; }
         public virtual System.Data.Entity.DbSet<Category> Categories { get; set; }
    }
} 