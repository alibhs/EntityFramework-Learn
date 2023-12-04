using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

ApplicationDbContext context = new();

#region Connection Resiliency Nedir ?
//Ef Core üzerinde yapılan veritabanı çalışmaları sürecinde ister sitemez veritabanlanı bağlantılarında kopuntu/kesinti vs. meydana gelebilmektedir.

//Connection Resiliency ile kopan bağlantıyı tekrar kurmak için gerekli tekrar bağlantı taleplerinde bulunabilir ve biryandan da execution strategy dediğimiz davranış modellerini belirleyerek bağlatınların kopma durumunda tekrar edecek olan sorugları baştan sona yeniden tetikleyebiliriz.

#endregion
#region EnableRetryOnFailure
// Veritabanı bağlantısı koptuğu taktirde bu yapılandırma sayesinde bağlantıyı tekrar kurmaya çalışabilriiz.
//while (true)
//{
//    await Task.Delay(200);
//    var persons = await context.Persons.ToListAsync();
//    persons.ForEach(p => Console.WriteLine(p.Name));
//    Console.WriteLine("******************");
//}
#region MaxRetryCount
//Yeniden bağlantı sağlanması durumunun kaç kere gerçekleşeceğini bildirmektedir.
//Default değeri 6'dir

#endregion
#region MaxRetryDelay
//Yeniden bağlantı sağlanması periyodunu bildirmektedir.
//Default değeri 30'dir
#endregion
#endregion

#region Execution Strategies
//Ef core ile yapılan bir işlem sürecinde veritabanı bağlantısı koptuğu taktirde yeniden bağlantı denenirken yapılan davranışa/alınan aksiyona denmektedir.

#region Default Execution Strategy
//Eğer ki enableretryonfailure metodu kullanılıyorsa bu metod otomatik kullanılır.
#endregion
#region Custom Execution Strategy
#region Kullanmak için ne yapılır?
//while (true)
//{
//    await Task.Delay(200);
//    var persons = await context.Persons.ToListAsync();
//    persons.ForEach(p => Console.WriteLine(p.Name));
//    Console.WriteLine("******************");
//}
#endregion
#endregion
#region Bağlantı Koptuğu Anda Execute Edilmesi Gereken Tüm Çalışmaları Tekrar İşlemek
//Ef core ile yapılan çalışma sürecinde veritabanı bağlantısının kesildği durumlarda, bazen bağlantının tekrar kurulması yetmemekte ve kesintinin olduğu çalışmanında başltan tekrardan işlenmesi gerekir. İşte bu tarz durumlarda EF Core Execute - ExecuteAsync fonksiyonu bizlere sunmaktadır.

//Execute fonksiyonu,içerisinde vermiş olduğumuz kodları commit edilene kadar tekrar işleyecektir.Eğer ki bağlantı kesilmesi meydana gelirse, bağlantının tekrardan kurulmasını durumunda Execute içerisindeki çalışmalar tekrar baştan işlenecek ve böylece yapılan işlemin tutarlılığı için gereklilik sağlanmış olacaktır.

var strategy = context.Database.CreateExecutionStrategy();
await strategy.ExecuteAsync(async () =>
{
    using var transcation = await context.Database.BeginTransactionAsync();

    await context.Persons.AddAsync(new() { Name = "Ali" });
    await context.SaveChangesAsync();

    await context.Persons.AddAsync(new() { Name = "Veli" });
    await context.SaveChangesAsync();

    await transcation.CommitAsync();
});

#endregion
#region Execution Strategy hangi durumlarda kullanılır ?

#endregion
#endregion
public class Person 
{
    public int PersonId { get; set; }
    public string Name { get; set; }
} 

class ApplicationDbContext:DbContext
{
    public DbSet<Person> Persons { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #region Default Execution Strategy
        //optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ApplicationDb;Trusted_Connection=true",
        //    builder => builder.EnableRetryOnFailure(
        //        maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(15), errorNumbersToAdd: new[] { 4060 }))
        //    .LogTo(filter: (eventId, level) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
        //    logger: eventData =>
        //    {
        //        Console.WriteLine($"Bağlantı tekrar kurulmaktadır");
        //    });
        #endregion
        #region Custom Execution Strategy
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=ApplicationDb;Trusted_Connection=true",
            builder => builder.ExecutionStrategy(
                dependencies => new CustomExecetionStrategy(dependencies, 3,TimeSpan.FromSeconds(15))));
           
        #endregion
    }

}

class CustomExecetionStrategy : ExecutionStrategy
{
    public CustomExecetionStrategy(DbContext context, int maxRetryCount, TimeSpan maxRetryDelay) : base(context, maxRetryCount, maxRetryDelay)
    {
    }
    int retryCount = 0;
    protected override bool ShouldRetryOn(Exception exception)
    {
        //Yeniden bağlantı durumunun söz konusu olduğu anlarda yapıalcak işlemler

        Console.WriteLine($"{++retryCount}. Bağlantı tekrar kuruluyor");
        return true;
    }
}