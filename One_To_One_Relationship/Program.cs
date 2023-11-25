// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine();

ESirketDbContext context = new();

#region Default Convention
//Her iki entity'de navigation property ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağını ifade edilir.
//One to One ilişki türünde, dependent entity'nin hangisi olduğunu default olarak belirlemek pek kolay değildir. Bu durumda fiziksel olarak bir foreign key'e karşılık property/kolon tanımlayarak çözebiliriz.
//Böylece foreign key'e karşılık property tanımlayarak lüzümsüz bir kolon oluşturmuş oluruz.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}

//class CalisanAdresi
//{
//    public int Id { get; set; }
//    public int CalisanId { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}
#endregion

#region Data Annotations
//Navigation Propertyler tanımlanmalıdır.
//Forein Key kolonunun ismi default convention'ın dışında bir kolon olacaksa eğer ForeignKey attribute ile bunu bildirebililiriz.
//Foreign Key kolonu oluşturulmak zorunda değildir.
//1'e 1 ilişikide ekstradan bir Foreign Key kolununa ihtiyaç olmayacağından dolayı dependent entity'deki id kolonunun hem foreign key hem de primary key olarak kullanmayı tercih edip özen gösteririz.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public CalisanAdresi CalisanAdresi { get; set; }
//}

//class CalisanAdresi
//{
//    [Key,ForeignKey(nameof(Calisan))] // 2. Yöntem = Id hem Pk hem FK oldu ekstra kolona gerek kalmadı maliyet azaldı
//    public int Id { get; set; }
//    //[ForeignKey(nameof(Calisan))] //1. yöntem maliyeti fazla
//    //public int CalisanId { get; set; } 
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; }
//}
#endregion

#region Fluent API
//Navigation propertler tanımlanmalıdır.
//Fluent API yönteminde entity'ler arasındaki ilişki context sınıfı içerisinde OnModelCreating fonks override edilerek metodlar aracılığyla tasarlanması gerekir. Yani tüm bu sorumluluk bu fonk içerisinde çalışmalıdır.

class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public CalisanAdresi CalisanAdresi { get; set; }
}

class CalisanAdresi
{
    public int Id { get; set; }
    public string Adres { get; set; }
    public Calisan Calisan { get; set; }
}
#endregion
class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<CalisanAdresi> CalisanAdresleri { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ESirketDb;Trusted_Connection=true");

    }
    //Model'lerin(entity) veritabanında genarete edilecek yapıları bu fonksiyon içinde konfügre edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalisanAdresi>().HasKey(c => c.Id);

        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c=>c.Id
            
            );
    }
}