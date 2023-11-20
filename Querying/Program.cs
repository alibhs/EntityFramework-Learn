using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;

Console.WriteLine();
ETicaretContext context = new();

#region En Temel Basit Bir Sorgu Nasıl Yapılır ?*
#region Method Syntax
//var urunler = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler2 = await (from urun in context.Urunler select urun).ToListAsync();
#endregion
#endregion

#region Oluşturulan Sorguyu Execute Etmek İçin Ne Yapmamız Gerekir ?
#region ToListAsync
#region Method Syntax
//var urunler3 = await context.Urunler.ToListAsync();
#endregion
#region Query Syntax
//var urunler4 = (from urun in context.Urunler select urun).ToListAsync();
#endregion
#endregion

//int urunId = 5;
//string urunAdi = "2";

//var urunler = from urun in context.Urunler
//              where urun.Id > urunId && urun.UrunAdi.Contains(urunAdi)
//              select urun;

//urunId = 200;
//urunAdi = "4";

//foreach (Urun urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}

#region Foreach
//foreach (var urun in urunler)
//{
//    Console.WriteLine(urun.UrunAdi);
//}
#region Deferred Execution(Ertelenmiş Çalışma)
//IQueryable çalışmalarında ilgili kod yazıldığı noktada tetiklenmez/çalıştırılmaz yani ilgili kod yazıldığı noktada sorguyu generate etmez!
//Nerede Eder ? => Çalıştırıldığı / execute edildiği noktada tetiklenir ! İşte bu duruma Ertelenmiş Çalışma denir

#endregion
#endregion
#endregion

#region IQueryable ve IEnumerable Nedir ? Basit Olarak !

//var urunler = (from urun in context.Urunler select urun).ToListAsync();

#region IQueryable
//Sorguya karşılık gelir.
//Ef core üzerinden yapılmış olan sorgunun execute edilmiş halini ifade eder.
#endregion
#region IEnumerable
//Veriler artık memoryde.
//Sorguların çalıştıırlıp/execute edilip verilerin in memorye yüklenmiş halini ifader eder.
#endregion
#endregion

#region Çoğul Veri Getiren Sorgulama Fonksiyonları
#region ToListAsync
//Üretilen sorguyu execute ettirmemizi sağlayan fonksiyondur.

#region Method Sytnax
//var urunler = await context.Urunler.ToListAsync();
#endregion

#region Query Syntax
//var urunler = (from urun in context.Urunler select urun).ToListAsync();
#endregion

#endregion

#region Where
//Oluşturulan sorguya where şartı eklememizi sağlayan fonksiyondur

#region Method Sytnax
//var urunler = await context.Urunler.Where(u => u.Id > 500).ToListAsync();
#endregion

#region Query Syntax
//var urunler = (from urun in context.Urunler where urun.Id > 500 && urun.UrunAdi.EndsWith("7") select urun).ToListAsync();
#endregion

#endregion

#region OrderBy
//Sorgu üzerinde sıralama yapmamızı sağlayan bir fonksiyondur

#region Method Sytnax
var urunler4 = await context.Urunler.Where(u => u.Id > 5 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi).ToListAsync();
#endregion

#region Query Syntax
//var urunler5 = (from urun in context.Urunler
//                where urun.Id > 5 || urun.UrunAdi.EndsWith("2")
//                orderby urun.UrunAdi
//                select urun).ToListAsync();
#endregion


#endregion

#region ThenBy
//Order By üzerinde yapılan sıralama işlemini farklı kolonlarda uygulamayı sağlayan bir fonksiyondur.

//var urunler6 = await context.Urunler.Where(u => u.Id > 5 || u.UrunAdi.EndsWith("2")).OrderBy(u => u.UrunAdi).ThenBy(u => u.Fiyat).ToListAsync();

#endregion

#region OrderByDescending
////Descinding olarak sılama yapmamızı sağlayan bir fonksiyondur
//var urunler8 = await context.Urunler.Where(u => u.Id > 5 || u.UrunAdi.EndsWith("2")).OrderByDescending(u => u.UrunAdi).ToListAsync();

//var urunler9 = (from urun in context.Urunler
//                where urun.Id > 5 || urun.UrunAdi.EndsWith("2")
//                orderby urun.UrunAdi descending
//                select urun).ToListAsync();

#endregion
#region ThenByDescending
//var urunler10 = await context.Urunler.Where(u => u.Id > 5 || u.UrunAdi.EndsWith("2")).OrderByDescending(u => u.UrunAdi).ThenByDescending(u=>u.Fiyat).ToListAsync();

#endregion


#endregion

#region Tekil Veri Getiren Sorgulama Fonksiyonları
//Yapılan sorguda sade ve sadece tek bir verinin gelmesi amaçlanıyorsa Single veya SingleOrDefault
//fonksiyonları kullanılabilir.
#region SingleAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa ya da hiç gelmiyorsa exeption
//fırlatır.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id == 2); //id no 2 olan ürün geldi
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id == 2323); //exception fırlattı böyle bir değer yok
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.SingleAsync(u => u.Id > 2); //exception fırlattı birden fazla değer var

#endregion
#endregion

#region SingleOrDefaultAsync
//Eğer ki, sorgu neticesinde birden fazla veri geliyorsa exeption fırlatır, hiç gelmiyorsa null döner.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.SingleOrDefault(u => u.Id == 2); //id no 2 olan ürün geldi
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.SingleOrDefault(u => u.Id == 2323); //exception fırlattı null değer döndürdü
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.SingleOrDefault(u => u.Id > 2); //exception fırlattı birden fazla değer var

#endregion
#endregion

//Yapılan sorguda tek bir verinin gelmesi amaçlanıyorsa First ya da FirstOrDefault fonksiyon kullanılır.

#region FirstAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa hata fırlatır.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id == 2); //id no 2 olan ürün geldi
#endregion
#region Hiç Kayıt Gelmediğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id == 2323); //exception fırlattı böyle bir değer yok
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.FirstAsync(u => u.Id > 2); //Id si 2 den büyük olan değerlerden ilkini getirir yani 3'ü

#endregion
#endregion

#region FirstOrDefaultAsync
//Sorgu neticesinde elde edilen verilerden ilkini getirir. Eğer ki hiç veri gelmiyorsa null değer döndürür.
#region Tek Kayıt Geldiğinde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 2); //id no 2 olan ürün geldi
#endregion
#region Hiç Kayıt Gelmediğinde
var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id == 2323); //null değer döndürdü
#endregion
#region Çok Kayıt Geldiğinde
//var urun = await context.Urunler.FirstOrDefaultAsync(u => u.Id > 2); //gelen kayıtlardan ilkini gösterir

#endregion

#endregion

#region SingleAsync,SingleOrDefaultAsync,FirstAsync,FirstOrDefaultAsync Karşılaştırılması
// C:\Users\Ali\Desktop\EntityFrameworkExamples\Single_First_Farkalrı.png
#endregion

#region FindAsync
//Find fonksiyonu, primary key kolonuna özel hızlı bir şekilde sorgulama yapmamızı sağlayan bir fonksiyondur.
//Urun urun2 = await context.Urunler.FindAsync(2);

#region Composite primary key durumu
//UrunParca urunparca = await context.UrunParca.FirstAsync(2, 5);
#endregion
#endregion

#region FindAsync İle  SingleAsync,SingleOrDefaultAsync,FirstAsync,FirstOrDefaultAsync Karşılaştırılması
// C:\Users\Ali\Desktop\EntityFrameworkExamples\Find_ile_first-single_karsilastirma.png
#endregion

#region LastAsync
//First Fonksiyonlarının aynısı ancak sondan getirir exception fırlatır. OrderBy Kullanılmalıdır!!!
//var urun = context.Urunler.OrderBy(u => u.UrunAdi).LastAsync(u => u.Id == 2);
#endregion

#region LastOrDefaultAsync
//First Fonksiyonlarının aynısı ancak sondan getirir null döndürür

#endregion

#endregion

#region Diğer Sorgulama Fonksiyonları

#region CountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini
//sayısal olarak (int) bizlere bildiren fonksiyondur.
//ToList yapıp sonra count yapabiliriz ama maaliyetli!!!
//var urunler = await context.Urunler.CountAsync(u=> u.Fiyat >7);
#endregion

#region LongCountAsync
//Oluşturulan sorgunun execute edilmesi neticesinde kaç adet satırın elde edileceğini
//sayısal olarak (long) bizlere bildiren fonksiyondur.
//var urunler = await context.Urunler.LongCountAsync(u=> u.Fiyat > 7);

#endregion

#region AnyAsync
//Sorgu neticesinde verinin gelip gelmediğini bool türünde dönen fonksiyondur.
//var urunler = await context.Urunler.AnyAsync(u=> u.UrunAdi.Contains("A")); // 1. yöntem
//var urunler = await context.Urunler.Where(u=> u.UrunAdi.Contains("A")).AnyAsync(); //2.Yöntem
#endregion

#region MaxAsync
//var fiyat = context.Urunler.MaxAsync(u => u.Fiyat);
#endregion

#region MinAsync
//var fiyat = context.Urunler.MinAsync(u => u.Fiyat);
#endregion

#region Distinct
//Sorguda mükerrer kayıtlar varsa bunları tekilleştiren bir işleve sahip fonksiyondur.
//var urunler = await context.Urunler.Distinct().ToListAsync();
#endregion

#region AllAsync
//Sorguda gelen verilerin, verilen şarta uyup uymadığını kontrol eder. Tüm şartlara uyuyorsa true
//uymayan varsa false değer döndürür.
//var m = await context.Urunler.AllAsync(u => u.Fiyat > 500);
#endregion

#region SumAsync

#endregion

#region AverageAsync

#endregion

#region ContainsAsync
//Like '%...%' sorgusu oluşturmamızı sağlar.
//var urunler = await context.Urunler.Where(u => u.UrunAdi.Contains("A").ToListAsync();
#endregion

#region StartsWith

#endregion


#region EndsWith

#endregion

#endregion

#region Sorgu Sonucu Dönüşüm Fonksiyonları
//Bu fonksiyonlar ile sorgu neticesinden elde edilen verileri isteğimiz doğrultusunda
//farklı türlere projecsiyon edebiliriz.

#region ToDictionaryAsync
//Sorgu neticesinde gelecek olan veriyi bir dictionary olarak elde etmek/tutmak/karşılaştırmak için kullanıılır.

//var urunler = await context.Urunler.ToDictionaryAsync(u=>u.UrunAdi,u=>u.Fiyat);

//ToList ile aynı amaca hizmet etmektedir.Yani, oluşturulan sorguyu execute edip neticesini anlar.
//ToList : Gelen sorgu neticesinde enttiy türünde bir koleksiyon(List<TEntity>) dönüştürmekteyken,
//ToDictionary ise : Gelen sorgu neticesini Dictionary türünden bir koleksiyona dönüştürücektir.
#endregion

#region ToArrayAsync
//Oluşturulan sorguyu dizi olarak elde eder.
//ToList ile muadil amaca hizmet eder. Yani sorguyu execute eder lakin sonucu
//entity dizisi olarak elde eder.

//var urunler = await context.Urunler.ToArrayAsync();
#endregion

#region Select
//Select fonksiyonun işlevsel olarak birden fazla davranışı söz konusudur.
//1. Select fonksiyonu,genarate edilecek sorgunun çekilecek kolonlarını ayarlamamızı sağlar.
//var urunler = await context.Urunler.Select(u=> new Urun
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat,
//}).ToListAsync();

//2. Select fonksiyonu, gelen verileri farklı türlerde karşılaştırmamızı sağlar. T,anonim
//var urunler = await context.Urunler.Select(u=> new
//{
//    Id = u.Id,
//    Fiyat = u.Fiyat,
//}).ToListAsync();



#endregion

#region SelectMany
//Selectle aynı amaca hizmet eder lakin,ilişkisel tablolar neticesinde gelen verileri koleksiyonel verileri
//tekelleştirip projeksiyon etmemizi sağlar.

//var urunler = await context.Urunler.Include(u=>u.Parcalar).
//    SelectMany(u=> u.Parcalar, (u,p)=> new
//    {
//        u.Id,
//        u.Fiyat,
//        p.ParcaAdi
//    })

//    .ToListAsync();
#endregion

#endregion

#region GroupBy Fonksiyonu
//Gruplama yapmamızı sağlayan fonksiyondur.
#region Method Syntax
//var datas = await context.Urunler.GroupBy(u => u.Fiyat).Select(group => new
//{
//    Count = group.Count(),
//    Fiyat = group.Key
//}).ToListAsync();
#endregion
#region Query Syntax
//var datas = await(from urun in context.Urunler
//            group urun by urun.Fiyat
//            into @group
//            select new
//            {

//                Fiyat = @group.Key,
//                Count = @group.Count()
//            }).ToListAsync();
#endregion
#endregion

#region Foreach Fonksiyonu
//Bir sorgulama fonksiyonu falan değildir!
//Sorgulama neticesinde elde edilen kolekyisyonel veriler üzerinde iteresyonel olarak dönmemizi
//ve teker teker verileri elde edip işlemler yapabilmemizi sağlayan bir fonksiyondur.foreach döngüsünün method.
//foreach (var item in collection)
//{

//}
//dataas.ForEach();
#endregion

public class ETicaretContext : DbContext
{
    public DbSet<Urun>? Urunler { get; set; }
    public DbSet<Parca> Parcalar { get; set; }
    public DbSet<UrunParca>? UrunParca { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IBBTC90;Database=EcommerceDb;Trusted_Connection=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrunParca>().HasKey(up => new { up.UrunId, up.ParcaId });
    }
}
public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public ICollection<Parca> Parcalar { get; set; }
}
public class Parca
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}
public class UrunParca
{
    public int UrunId { get; set; }
    public int ParcaId { get; set; }

    public Urun Urun { get; set; }
    public Parca Parca { get; set; }
}

public class UrunDetay
{
    public int Id { get; set; }
    public float Fiyat { get; set; }
}