// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Mime;

Console.WriteLine("Hello, World!");

#region Veri Nasıl Güncellenir ?
//ETicaretContext context = new();

// Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 3);
// urun.UrunAdi = "H Urunu";
// urun.Fiyat = 2341;

// await context.SaveChangesAsync();

#endregion
#region ChangeTracker Nedir ? Kısaca !
//ChangeTracker context üzerinden gelen verilerin takibinden sorumlu bir mekanizmadır. Bu takip mekanizması sayesinde context  üzerinden gelen
//verilerle ilgili işlemler neticesinde update ya da delete sorgularının oluşturulacağı anlaşılır!

#endregion
#region Takip Edilmeyen Nesneler Nasıl Güncellenir ?
//ETicaretContext context = new();
//Urun urun = new()
//{
//    Id = 3,
//    UrunAdi = "Yeni Urun",
//    Fiyat = 123
//};
//#region Update Fonksiyonu
////bu fonksiyon context üzeründen getirmediğimiz için changetracker çalışmaz ve update metodu ile güncellenir
////Update fonksiyonu kullanabilmek için kesinlikle ID değeri verilmelidir !!!!!! hangi verinin güncelleneceğini anlaması için

//context.Urunler.Update(urun);
//await context.SaveChangesAsync();
//#endregion
#endregion
#region EntityState Nedir ?
//Bir entity instance'ının durumunu ifade eden bir referanstır.
//ETicaretContext context = new();
//Urun u = new();
//Console.WriteLine(context.Entry(u).State);
#endregion
#region Ef Core Açısından bir verinin güncellenmesi gerektiğini nasıl anlarız ?
//ETicaretContext context = new();
//Urun urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id ==3);
//Console.WriteLine(context.Entry(urun).State);
//urun.UrunAdi = "Hilmi";
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);
#endregion
#region Birden Fazla Veri Güncellerken Nelere Dikkat Edilmelidir ?
//Savechanges maliyetli olduğunda tekrar tekrar kullanma
ETicaretContext context = new();
var urunler = await context.Urunler.ToListAsync();
foreach (var urun in urunler)
{
    urun.UrunAdi += "*";
    urun.Fiyat += 9;
    await context.SaveChangesAsync();
}
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

