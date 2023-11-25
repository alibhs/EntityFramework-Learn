
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine();

ESirketDbContext context = new();

#region Default Convention
//Default c. yönteminde bire çok ilişki kurarken foreign key kolonuna karşılık gelen bir property tanımlamak zorunda değiliz.Eğer tanımlamazsak Ef Core kendisi tanımlar. Tanımlarsak tanımladığımızı baz alır.
//class Calisan
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }

//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }

//}
#endregion

#region Data Annotations
//Default c. yönteminde foreing key kolonuna karşılık gelen property tanımladığımızda bu property ismi temel geleneksel entity tanımlama kurallarına uymuyorsa eğer Data Annotations ile müdahelede bulanabilir.

//class Calisan
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Departman))]
//    public int DId { get; set; }
//    public string Adi { get; set; }
//    public Departman Departman { get; set; }


//}

//class Departman
//{
//    public int Id { get; set; }
//    public string DepartmanAdi { get; set; }
//    public ICollection<Calisan> Calisanlar { get; set; }


//}
#endregion

#region Fluent API
class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public Departman Departman { get; set; }


}

class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }
    public ICollection<Calisan> Calisanlar { get; set; }


}
#endregion
class ESirketDbContext : DbContext
{
    public DbSet<Calisan> Calisanlar { get; set; }
    public DbSet<Departman> Departmanlar { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ESirketDb;Trusted_Connection=true");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.Departman)
            .WithMany(d => d.Calisanlar);
    }
}