using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using System.Runtime.CompilerServices;

Console.WriteLine();

ETicaretContext context = new();

#region ChangeTracker Nedir ?
//Context nesnesi üzerinden gelen tüm nesneler/veriler otomatik bir takip mekanizması tarafından izlenirler.Bu Takip Mekanizmasına denir.
//ChangeTracker ile nesneler üzerindeki işlemler/değişikler takip edilerek netice itibariyle bu işlemler fıtratına uygun sql sorgucukları genarete edilir. bu işleme Change Tracking denir
#endregion

#region ChangeTracker Propertysi
//Takip edilen nesnelere erişebilmemizi sağlayan ve gerektiği taktirde işlemler gerçekleştirmemizi sağlayan bir property'dir.
//Context Sınıfının base classı olan DbContext sınıfının bir member'ıdır.
//var urunler = await context.Urunler.ToListAsync();

//urunler.FirstOrDefaultAsync(u => u.Id == 3).Fiyat = 123;
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 1));

//var datas = context.ChangeTracker.Entries();



#region DetectChanges Metodu
//EF Core, context nesnesi tarafından izlenen tüm değişkenleri ChangeTracker sayesinden takip edebilmekte ve nesnelerde olan verisel değişiklikler yakalanarak bunların anlık görüntüleri(snapshot)'ını
//oluşturabilir.
//Yapılan değişikliklerin veritabanına gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges fonk çağrıldığı anda nesneler Ef Core tarafından otomatik kontrol edilirler.
//Ancak yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için değişiklerin algılanmasını opsiyonel olarak gerçekleştirmek isteyebiliriz. İşte bunun için detectchanges kullanılabilir.

//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id ==3);
//urun.Fiyat = 123;

//context.ChangeTracker.DetectChanges();
//await context.SaveChangesAsync();

#endregion

#region AutoDetectChangesEnabled Property'si
//İlgili metodlar (SaveChanges,Entries) tarafından DetectChanges methodunun otomatik olarak tetiklenmesinin konfigrasyonunu yapmamızı sağlayan methoddur.
//SaveChanges methodu tetiklendiğinde DetectChanges methodunu içerisinde default olarak çağırmaktadır. Bu durumda DetectChanges fonk kullanımını irademizle yonetmekle ve maaliyet/performans optimizasyonu
//yapmak istediğimşzde AutoDetectChangesEnabled metodunu kapatabiliriz.
#endregion

#region Entries Methodu
//Context'teki Entry methodunun koleksiyonel versiyonudur.
//Change Tracker mekanizması tarafından izlenen her entity nesnesinin bilgisini EntityEntry türünden elde etmemizi sağlar ve belirli işlemler yapmamıza olanak sağlar.
//Entries methodu çalışmadan önce DetectChanges metodunu tetikler.Bu durumda tıpki SaveChanges da olduğu gibi maliyettir. Maliyetten kaçınmak için AutoDetectChangesEnabled false değeri verilmelidir.

//var urunler = await context.Urunler.ToListAsync();

//urunler.FirstOrDefault(u => u.Id == 3).Fiyat = 123;
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 1));
//context.ChangeTracker.Entries().ToList().ForEach(e =>
//{
//    if (e.State == EntityState.Unchanged)
//    {
//        //:...
//    }
//    else if(EntityState.Deleted)
//    {
//        //:...
//    }
//});

#endregion

#region AcceptAllChanges Metodu
//SaveChanges() veya SaveChanges(true); olarak tetiklendiğinde Ef Core her şeyin yolunda olduğunu varsayarak track ettiği verilerin takibini keser yeni değişikliklerin takip edilmesini bekler.
//Böyle bir durumda beklenmeyen bir durum/olası bir hata söz konusu olursa eğer Ef Core takip ettiği nesneler bırakacağı için bir düzeltme mevzu bahis olmayacaktır.

//Haliyele bu durumda SaveChanges(false) ve AcceptAllChanges metodları devreye girecektir.

//SaveChanges(false), Ef Core'a gerekli veritabanı komutlarını yüklemesini söyler ancak gerektiğinde yeniden oynatılabilmesi için değişiklikleri beklemeye/nesneleri takip etmeye devam eder. Ta ki
//AcceptAllChanges methodunu irademizle çağırana kadar!


//var urunler = await context.Urunler.ToListAsync();

//urunler.FirstOrDefault(u => u.Id == 3).Fiyat = 123;
//context.Urunler.Remove(urunler.FirstOrDefault(u => u.Id == 1));
//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();
#endregion

#region HasChanges Metodu
//Takip edilen nesneler arasında değişiklik var olup olmadığını bilgisini verir. Arkaplanda DetectChanges methodunu tetikler.
//var result = context.ChangeTracker.HasChanges();
#endregion
#endregion

#region Entity State
//Entity nesnelerin durumlarını ifade eder.

#region Detached
//Nesnenin  change tracker mekanizması tarafından kontrol edilmediğini ifade eder.
//Urun urun = new();
//Console.WriteLine(context.Entry(urun).State);
#endregion

#region Added
//Veritabanını eklenecek nesneyi ifade eder. Added henüz veritabanına işlenmeyen veriyi ifade eder.Savechanges fonksşyonu cağırıldığında insert sorgusu oluşturulacağını anlamına gelir.
//Urun urun = new() { Fiyat = 31,UrunAdi = "kablo"  };
//Console.WriteLine(context.Entry(urun).State);
//await context.Urunler.AddAsync(urun);
//context.SaveChangesAsync();
#endregion

#region Unchanged
//Veritabanında sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını belli eder.Sorgu neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir.
#endregion

#region Modified
//Nesne üzerinden güncelleme yani değişiklik yapıldığını ifade eder. Savechanges fonk çağırıldığında update sorgusu oluşturulacağı anlamına gelir.

#endregion

#region Deleted

#endregion

#endregion

#region ChangeTracker'ın Interceptor Olarak Kullanılması

#endregion

#region Context Nesnesi Üzerinden Change Tracker
// context.ChangeTracker. ile çoğul state kontrol edilebilir.
//context.Entry diyerek tekil state kontrol edilebilir.

var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 5);
urun.Fiyat = 123;
urun.UrunAdi = "kablo2"; //Modified / Update

#region Entry Metodu
#region OriginalValues Propertysi
//nesnenin orjinal değerini verir
var fiyat = context.Entry(urun).OriginalValues.GetValue<float>(nameof(Urun.Fiyat));
#endregion

#region CurrentValues Propertysi
////nesnenin o anki tempdeki değerini verir

context.Entry(urun).CurrentValues.GetValue<string>(nameof(Urun.UrunAdi));
#endregion

#region GetDatabaseValues Propertysi
//nesnenin veritabanındaki değerini verir

#endregion
#endregion
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
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                //;..
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }
}