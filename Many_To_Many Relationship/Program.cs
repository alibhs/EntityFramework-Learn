

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

Console.WriteLine();

#region Default Convention
//İki entity arasındaki ilişkiyi navigation propertyler üzerinden çoğul olarak kurmalıyız.(ICollection, List)
//Default Convention'da cross table'ı manuel oluşturmak zorunda değiliz Ef Core otomatik yapar.
//Ve oluşturulan cross table'ın içerisinde composite primary key de otomatik oluşturulacaktır.
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<Yazar> Yazarlar { get; set; }

//}

//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<Kitap> Kitaplar { get; set; }


//}
#endregion

#region Data Annotations
//Cross table manuel olarak oluşturulmak zorundadır.
//Entity'leri oluşturduğumuz cross table entitysi ile bire çok bir ilişki kurulmalıdır.
//CT'da composite primary key'i data annotation(Attributes)lar ile manuel kuramıyoruz.Bunu için de Fluent API'da çalışma yapmamız gerekir.
//Cross table'a karşılık bir entity modeli oluşturuyorsak eğer bunu DbSet property'si olarak bildirmek mecburiyetinde değiliz.
//class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }
//    public ICollection<KitapYazar> Yazarlar { get; set; }

//}
//Cross Table
//class KitapYazar
//{
//    public int KitapId { get; set; }
//    public int YazarId { get; set; }
//    public Kitap Kitap { get; set; }
//    public Yazar Yazar { get; set; }
//}
//class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    public ICollection<KitapYazar> Kitaplar { get; set; }


//}
#endregion

#region Fluent API
//Cross table manuel olarak oluşturulmalı
//DbSet olarak eklenmesine lüzüm yok,
//Composite PK Haskey Metodu ile kurulmalı!.

class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }

}
class KitapYazar
{
    public int KitapId { get; set; }
    public int YazarId { get; set; }
    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }
}
class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }


}
#endregion

class EKitapDbContext : DbContext
{
    public DbSet<Kitap> Kitaplar { get; set; }
    public DbSet<Yazar> Yazarlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=EKitapDb;Trusted_Connection=true");

    }
    //Data Annotations
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<KitapYazar>()
    //        .HasKey(ky => new {ky.KitapId,ky.YazarId});
    //}

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky => new { ky.KitapId, ky.YazarId });

        modelBuilder.Entity<KitapYazar>().
            HasOne(ky => ky.Kitap)
            .WithMany(k => k.Yazarlar)
            .HasForeignKey(ky=> ky.KitapId);

        modelBuilder.Entity<KitapYazar>().
           HasOne(ky => ky.Yazar)
           .WithMany(k => k.Kitaplar)
           .HasForeignKey(ky=> ky.KitapId);
    }
}