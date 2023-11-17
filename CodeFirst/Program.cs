
using Microsoft.EntityFrameworkCore;

ECommerceDbContext context = new();
await context.Database.MigrateAsync();

public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=CodeFirst;Trusted_Connection=true");
    }
}

//Entity
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int  Quantity{ get; set; }
    public float Price { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}

     




