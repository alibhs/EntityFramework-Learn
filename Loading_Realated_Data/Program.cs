using Loading_Related_Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();
#region Loading Related Data

#region Eager Loading
//Eager loading, generate edilen bir sorguya ilişkisel verilerin parça parça eklenmesini sağlayan ve bunu yaparken iradeli/istekli bir şekilde yapmamızı sağlayan bir yöntemdir.

#region Include
//Eager loading operasyonunu yapmamızı sağlayan bir fonksiyondur.
//Yani üretilen bir sorguya diğer ilişkisel tabloların dahil edilmesini sağlayan bir işleve sahiptir..

//var employees = await context.Employees.Include("Orders").ToListAsync(); genelde kullanılmayan yöntem tip güvenli değil

//var employees = await context.Employees.
//    Where(e => e.Orders.Count > 2)
//    .Include(e => e.Orders).
//    Include(e =>e.Region).
//    ToListAsync();



#endregion
#region ThenInclude
//ThenInclude, üretilen sorguda Include edilen tabloların ilişkili olduğu diğer tablolarıda sorguya ekleyebilmek için kullanılan bir fonksiyondur. 
//Eğer ki, üretilen sorguya include edilen navigatiobn property koleksionel bir propertyse işte o zaman bu property üzerinden diğer ilişkisel tabloya erişim gösterilememektedir. Böyle bir durumda koleksiyonel propertylerin türlerine erişip, o tür ile ilişkili diğer tablolarıda sorguya eklememizi sağlayan fonksiyondur.

//var orders = await context.Orders.Include(o=>o.Employee)
//    .Include(o=> o.Employee.Region)
//    .ToListAsync();

//var regions = await context.Regions
//    .Include(r => r.Employees)
//        .ThenInclude(e => e.Orders)
//    .ToListAsync();

#endregion
#region Filtered Include
//Sorgulama süreçlerinde Include yaparken sonuçlar üzerinde filtreleme ve sıralama gerçekleştirebilmemiz isağlayan bir özleliktir.

var regions = await context.Regions.Include(r=> r.Employees.Where(e => e.Name.Contains("a"))).ToListAsync();

//Desktelenen fonksiyon : Where, OrderBy, OrderByDescending, ThenBy, ThenByDescending, Skip, Take

//Change Tracker'ın aktif olduğu durumlarda Include ewdilmiş sorgular üzerindeki filtreleme sonuçları beklenmeyen olabilir. Bu durum, daha önce sorgulanmş ve Change Tracker tarafından takip edilmiş veriler arasında filtrenin gereksinimi dışında kalan veriler için söz konusu olacaktır. Bundan dolayı sağlıklı bir filtred include operasyonu için change tracker'ın kullanılmadığı sorguları tercih etmeyi düşünebilirsiniz.

#endregion
#region Eager Loading İçin Kritik Bir Bilgi
//EF Core, önceden üretilmiş ve execute edilerek verileri belleğe alınmış olan sorguların verileri, sonraki sorgularda KULLANIR!

//var orders = await context.Orders.ToListAsync();

//var employees = await context.Employees.ToListAsync();

#endregion
#region AutoInclude - EF Core 6
//Uygulama seviyesinde bir entitye karşılık yapılan tüm sorgulamalarda "kesinlikle" bir tabloya Include işlemi gerçekleştirlecekse eğer bunu her bir sorgu için tek tek yapmaktansa merkezi bir hale getirmemizi sağlayan özelliktir.

//var employees = await context.Employees.ToListAsync();
#endregion
#region IgnoreAutoIncludes
//AutoInclude konfigürasyonunu sorgu seviyesinde pasifize edebilmek için kullandığımız fonksiyondur.

//var employees = await context.Employees.IgnoreAutoIncludes().ToListAsync();
#endregion
#region Birbirlerinden Türetilmiş Entity'ler Arasında Include

#region Cast Operatörü İle Include
var persons = context.Persons.Include(p => ((Employee)p).Orders).ToListAsync();
#endregion
#region as Operatörü İle Include
//var persons = context.Persons.Include(p => (p as Employee).Orders).ToListAsync();

#endregion
#region 2. Overload İle Include
//var persons = context.Persons.Include("Orders").ToListAsync();

#endregion
#endregion


Console.WriteLine();
#endregion


#region Explicit Loading
//Oluşturulan sorguya eklenecek verilerin şartlarına bağlı bir şekilde /ihtiyaçlara istinadedn yüklenmesini sağlayan yaklaşımdır.

//Explicitsiz
//var employee = await context.Employees.FirstOrDefaultAsync(e=> e.Id == 2 );
//if(employee.Name == "Gençay")
//{
//    var orders = await context.Orders.Where(o => o.EmployeeId == employee.Id).ToListAsync();
//}


#region Collection Fonksiyonu
//Explicit loading sürecinde ilişkisel olarak sorguya eklenmek istenen prop eğer ki çoğul yani koleksiyonel bir türse bu tabloyu Collection ile sorguya ekleyebiliriz.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//...
//...

//await context.Entry(employee).Collection(e => e.Orders).LoadAsync();


#endregion
#region Reference Fonksiyonu
//Explicit loading sürecinde ilişkisel olarak sorguya eklenmek istenen prop eğer ki tekel bir türse bu tabloyu reference ile sorguya ekleyebiliriz.

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//await context.Entry(employee).Reference(e => e.Region).LoadAsync();
#endregion
#endregion
#region Collectiınlarda Aggragate operatoru uygulamak

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////...
////...

//await context.Entry(employee).Collection(e => e.Orders).Query().CountAsync();
#endregion
#region Collectiınlarda Filtreleme  uygulamak

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
////...
////...

//await context.Entry(employee).Collection(e => e.Orders).Query().Where(q => q.OrderDate.Day == DateTime.Now.Day).ToListAsync();
#endregion
#region Lazy Loading

#region N + 1 Problemi

#endregion
#endregion
#endregion



public class Person
{
    public int Id { get; set; }

}
public class Employee : Person
{
    //public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }

    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }

    public Employee Employee { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Region> Regions { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<Employee>()
            .Navigation(e => e.Region)
            .AutoInclude();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ApplicationDB;Trusted_Connection=true");
    }
}