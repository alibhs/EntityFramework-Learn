// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine();

public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=CodeFirst;Trusted_Connection=true");
        //Provider
        //ConnectionStrings
        //Lazy Loading
        //vb.
    }
}

public class Urun
{
    public int Id { get; set; }
}

#region OnConfiguring İle Konfigrasyon Ayarlarını Gerçekleştirmek
//EF Core tool'unu yapılandırmak için kullandığımız bir metottur.
//Context nesnesinde override edilerek kullanılır.
#endregion

#region Basit Düzeyde Entity Tanımlama Kuralları
//EF Core, her tablonun default olarak bir primary key kolonu olması gerektiğini kabul eder!
//Haliyle, bu kolonu temsil eden bir property tanımlamadığımız taktirde hata verecektir!
#endregion

#region Tablo Adını Belirleme

#endregion