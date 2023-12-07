using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System;
ApplicationDbContext context = new();
#region Ef Core Select Sorgularını Güçlendirme Teknikleri
#region IQueryable - IEnumerable Farkı
//IQueryable, bu arayüz üzerinde yapılan işlemler direkt genarete edilecek olan sorguya yansıtılacaktır.
//IEnumerable, bu arayüz üzerinde yapılan işlemler temel sorgu neticesinde gelen ve in-memory'e yüklenen instance'lar üzerinden gerçekleşir. Yabni Sorgu yansıtılmaz.

//IQueryable ile yapılan sorgulama çalışmalarından sql sorguyu hedef veriler elde edilecekken, IEnumurable ile yapılan sorgulama çalışmalarda sql daha geniş verileri getirebilecek şeklide execute ederek hedef veriler in-memory'de ayıklanır.

//IQueryable hedef verileri getirirken, hedef verilerden daha fazlasını getirip in-memory'de saklar.

//context.Persons.Where() //IQueryable

#region IQuaryable
//var persons = await context.Persons.Where(p => p.Name.Contains("a"))
//    .Take(3)
//    .ToListAsync();


//var persons = await context.Persons.Where(p => p.Name.Contains("a"))
//    .Where(p => p.PersonId > 3)
//    .Take(3)
//    .Skip(4)
//    .ToListAsync();

#endregion
#region IEnumurable
//AsEnumarable sonrası yapılan işlemler veritabanına yansıtılmaz in-mermoydeki veriler üzerinden gerçekleşir.
//Async metodları Enumurablede çalışmaz senkron olanlar kullanılır.

//var persons = context.Persons.Where(p => p.Name.Contains("a"))
//    .AsEnumerable()
//    .Take(3)
//    .ToList();
#endregion

#region AsQueryable
//IEnumurable sorgularını Queryableye çevirmeye yarar
#endregion
#region AsEnumerable
//Queryableye sorgularını IEnumurable çevirmeye yarar

#endregion

#endregion

#region Yalnızca İhtiyaç Olan Kolonları Listeleyin - Select
//var persons = await context.Persons.ToListAsync(); //Bütün verileri getirir maliyetli

//var persons = await context.Persons.Select(p => new
//{
//    p.Name,
//    p.PersonId
//}).ToListAsync();

#endregion

#region Result'ı Limitleyin - Take
//var persons = await context.Persons.ToListAsync(); //Bütün verileri getirir maliyetli büyük veriler varsa take ile parçalı getirmek daha perfomanslı olur.

//await context.Persons.Take(50).ToListAsync();
#endregion

#region Join Sorgularında Eager Loading Sürecinde Verileri Filtreleyin
//Join atılacak data filtrelenecekse tüm dataları getirme.

//Tüm Verileri Getirip Sonra Filtreledik Maaliyetli
//var persons = await context.Persons.Include(p => p.Orders)
//    .ToListAsync(); //Maaliyetli

//foreach (var person in persons)
//{
//    var orders = person.Orders.Where(o => o.CreatedDate.Year == 2022);
//}

//Daha Performanslı hali Include içinde filtreledik.

//var persons = await context.Persons.Include(p=> p.Orders.Where(o=>o.OrderId % 2 == 0)
//.OrderByDescending(o=> o.OrderId)
//.Take(4))
//.ToListAsync();

#endregion

#region Şartlara Bağlı Join Yapılacaksa Eğer Explicit Loading Kullanın
//Sadece ayşe için bütün orderları getirmeye gerek yok maliyetlidir
//var person = await context.Persons.
//    Include(p=>p.Orders).
//    FirstOrDefaultAsync(p => p.PersonId == 1);

//if(person.Name == "Ayşe")
//{
//    //Orderları getir...
//}

//İlk olarak include kaldırılır daha sonra şart durumunda include edilir

//var person = await context.Persons.
//    FirstOrDefaultAsync(p => p.PersonId == 1);

//if (person.Name == "Ayşe")
//{
//    //Orderları getir...
//    await context.Entry(person).Collection(p => p.Orders).LoadAsync();
//}

#endregion

#region Lazy Loading Kullanırken Dikkatli Olun!
#region Riskli Durum
//Her persons için orderları tekrar tekrarn baştan getirir

//var persons = await context.Persons.ToListAsync();

//foreach (var person in persons)
//{
//    foreach (var order in person.Orders)
//    {
//        Console.WriteLine($"{person.Name} - {order.OrderId}");
//    }
//    Console.WriteLine("*************");
//}
#endregion
#region İdeal Durum
//Orderı bir kere çekecez ve her seferinde onu kullanıcaz.

//var persons = await context.Persons.Select(p => new {p.Name , p.Orders}).ToListAsync();

//foreach (var person in persons)
//{
//    foreach (var order in person.Orders)
//    {
//        Console.WriteLine($"{person.Name} - {order.OrderId}");
//    }
//    Console.WriteLine("*************");
//}
#endregion
#endregion

#region İhtiyaç Noktalarında Ham SQL Kullanın - FromSql

#endregion

#region Asenkron Fonksiyonları Tercih Edin

#endregion

#endregion
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public virtual Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
              optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ApplicationDb;Trusted_Connection=true")
            .UseLazyLoadingProxies();
    }
}
