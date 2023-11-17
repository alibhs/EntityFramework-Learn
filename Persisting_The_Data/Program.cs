// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Mime;

Console.WriteLine();

#region Veri Nasıl Eklenir ?
//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "A Ürünü",
//    Fiyat = 1000 
//};

#region context.AddASync Fonksiyonu
//await context.AddAsync(urun); 1. Yöntem
//await context.SaveChangesAsync();

#endregion
#region context.DbSet.AddSync Fonksiyonu
//await  context.Urunler.AddAsync(urun); // 2. Yöntem
//await context.SaveChangesAsync();

#endregion

#endregion
#region SaveChanges Nedir ?
//SaveChanges; inser,update ve delete sorgularını oluşturup bir transaction eşliğinde veritabanına gönderip
//execute eden fonksiyondur. Eğer ki oluşturulan sorgulardan herhangi biri başarısız olursa tüm işlemleri geri alır(rollback)
#endregion

#region Ef Core Açısından bir verinin eklenmesi gerektiği nasıl anlaşılır ?
//ETicaretContext context = new();
//Urun urun = new()
//{
//    UrunAdi = "B ürünü",
//    Fiyat = 200
//};

//Console.WriteLine(context.Entry(urun).State);
//await context.AddAsync(urun);
//Console.WriteLine(context.Entry(urun).State);
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(urun).State);


#endregion

#region Birden fazla veri eklerken nelere dikkat edilmelidir ? 
ETicaretContext context = new();
Urun urun1 = new()
{
    UrunAdi = "C ürünü",
    Fiyat = 200
};
Urun urun2 = new()
{
    UrunAdi = "D ürünü",
    Fiyat = 200
};
Urun urun3 = new()
{
    UrunAdi = "E ürünü",
    Fiyat = 200
};

//context.AddRangeAsync(urun1, urun2, urun3); //farklı kaydetme yöntemi çok kullanılmaz 

await context.AddAsync(urun1);
await context.AddAsync(urun2);
await context.AddAsync(urun3);
await context.SaveChangesAsync();


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

