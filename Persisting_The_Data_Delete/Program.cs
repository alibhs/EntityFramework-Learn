// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

#region Veri Nasıl Silinir?
//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 1);

//context.Urunler.Remove(urun);
//await context.SaveChangesAsync();


#endregion
#region Silme İşleminde ChangeTracker'ın Rolü
//ChangeTracker context üzerinden gelen verilerin takibinden sorumlu bir mekanizmadır. Bu takip mekanizması sayesinde context  üzerinden gelen
//verilerle ilgili işlemler neticesinde update ya da delete sorgularının oluşturulacağı anlaşılır!
#endregion
#region Takip Edilmeyen Nesneler Nasıl Silinir ?
//ETicaretContext context = new();
//Urun u = new()
//{
//    Id = 12,
//};
//context.Urunler.Remove(u);
#endregion


public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ETicaretDb;Trusted_Connection=true");
        //Provider
        //ConnectionStrings
        //Lazy Loading
        //vb.
    }
}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
}

