
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

ApplicationDbContext context = new();

#region Complext Query Operators

#region Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//            on photo.PersonId equals person.PersonId
//            select new
//            {
//                person.Name,
//                photo.Url
//            };
//await query.ToListAsync();

#endregion
#region Method Syntax
//var query = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo, person) => new
//    {
//        person.Name,
//        photo.Url
//    });
//var datas = await query.ToListAsync();
#endregion

#region Multiple Columns Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//            on new { photo.PersonId, photo.Url } equals
//            new { person.PersonId, Url = person.Name }
//            select new
//            {
//                person.Name,
//                photo.Url
//            };
//var datas = await query.ToListAsync();
#endregion
#region Method Syntax
//var query = context.Photos
//    .Join(context.Persons,
//    photo => new
//    {
//        photo.PersonId,
//        photo.Url
//    },
//    person => new
//    {
//        person.PersonId,
//        Url = person.Name
//    },
//    (photo,person) => new
//    {
//        person.Name,
//        photo.Url
//    }
//    );
//var datas = await query.ToListAsync();
#endregion
#endregion

#region 2'den Fazla Tabloyla Join

#region Query Syntax
//var query = from photo in context.Photos
//            join person in context.Persons
//            on photo.PersonId equals person.PersonId
//            join order in context.Orders
//            on person.PersonId equals order.PersonId
//            select new
//            {
//                person.Name,
//                photo.Url,
//                order.Description
//            };
//var datas = await query.ToListAsync();
#endregion
#region Method Syntax
//ilk join içindeki anonim metoda personId yi eklememizin sebebi ikinci join sorgusunda personPhotostan erişebilmek
//var query = context.Photos
//    .Join(context.Persons,
//    photo => photo.PersonId,
//    person => person.PersonId,
//    (photo, person) => new
//    {
//        person.PersonId,
//        person.Name,
//        photo.Url
//    })
//    .Join(context.Orders,
//    personPhotos => personPhotos.PersonId,
//    order=> order.PersonId,
//    (personPhotos, order) => new
//    {
//        personPhotos.Name,
//        personPhotos.Url,
//        order.Description
//    });
#endregion

#region Group Join - GroupBy Değil!
//personOrders = orderları gruplar
//var query = from person in context.Persons
//            join order in context.Orders
//            on person.PersonId equals order.OrderId
//            into personOrders
//            /*from order in personOrders*/ // tekrardan orderlara erişebilmek için
//            select new
//            {
//                person.Name,
//                Count = personOrders.Count(),
//                personOrders
//            };

//var datas = await query.ToListAsync();

#endregion
#endregion

//DefaultEmpty:Sorgulama sürecinde ilişkisel olarak karşılığı olmayan verilere default değeri atayan yani LEFT JOIN sorugusu oluşturan bir fonksiyondur

#region Left Join
//var query = from person in context.Persons
//            join order in context.Orders
//            on person.PersonId equals order.PersonId
//            into personOrders
//            from order in personOrders.DefaultIfEmpty()
//            select new
//            {
//                person.Name,
//                order.Description
//            };
//var datas = await query.ToListAsync();
#endregion

#region Right Join
//var query = from order in context.Orders
//            join person in context.Persons
//            on order.PersonId equals person.PersonId
//            into orderPersons
//            from person in orderPersons.DefaultIfEmpty()
//            select new
//            {
//                person.Name,
//                order.Description
//            };
//var datas = await query.ToListAsync();
#endregion

#region Full Join
//var leftQuery = from person in context.Persons
//                join order in context.Orders
//                on person.PersonId equals order.PersonId
//                into personOrders
//                from order in
//                personOrders.DefaultIfEmpty()
//                select new
//                {
//                    person.Name,
//                    order.Description
//                };

//var rightQuery = from order in context.Orders
//                 join person in context.Persons
//                  on order.PersonId equals person.PersonId
//                into orderPersons
//                 from person in
//                 orderPersons.DefaultIfEmpty()
//                 select new
//                 {
//                     person.Name,
//                     order.Description
//                 };
//var fullJoin = leftQuery.Union(rightQuery);

//var datas = await fullJoin.ToListAsync();
#endregion

#region Cross Join
//var query = from order in context.Orders
//            from person in context.Persons
//            select new
//            {
//                order,
//                person
//            };

//var datas = await query.ToArrayAsync();
#endregion

#region Collection Selector'da Where Kullanma Durumu
//var query = from order in context.Orders
//            from person in context.Persons
//            .Where(p=> p.PersonId == order.OrderId)
//            select new
//            {
//                order,
//                person
//            };

//var datas = await query.ToListAsync();
#endregion

#region Cross Apply
//Inner Join 

//var query = from person in context.Persons
//            from order in context.Orders.Select(o => person.Name)
//            select new
//            {
//                person,
//                order
//            };
#endregion

#region Outer Apply
//Left Join
//var query = from person in context.Persons
//            from order in context.Orders.Select(o => person.Name).DefaultIfEmpty()
//            select new
//            {
//                person,
//                order
//            };
#endregion
#endregion
#endregion
Console.WriteLine();
public class Photo
{
    public int PersonId { get; set; }
    public string Url { get; set; }

    public Person Person { get; set; }
}
public enum Gender { Man, Woman }
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }

    public Photo Photo { get; set; }
    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ApplicationDb;Trusted_Connection=true");
    }

}