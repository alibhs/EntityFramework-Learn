using Microsoft.EntityFrameworkCore;
Console.WriteLine();

#region Relationships(İlişkiler) Terimleri
#region Principal Entity(Asıl Entity)
//Kendi başına var olabilen tabloyu modelleyen entity'e denir.
//Departmanlar tablosunu modelleyen 'Departman' Entity'sidir.
#endregion

#region Dependent Entity(Bağımlı Entity)
//Kendi başına var olamayan bir başka tabloya bağımlı (ilişkisel olarak bağımlı) olan tabloyu modelleyen entity'e denir.
//Çalışanlar tablosunu modelleyen 'Çalışan' entity'sidir.
#endregion

#region Foreign Key
//Principal Entity ile Dependent Entity arasındaki ilişkiyi sağlayan key'dir.

//Dependent Entity'de tanımlanır.
//Principal Entity'deki Principal Key'i tutar.
#endregion

#region Principal Key
//Principal Entity'deki Id'nın kendisidir.
//Principal Entity'nin kimliği olan kolonu ifade eden propertydir.
#endregion

class Calisan
{
    public int Id { get; set; }
    public string CalisanAdi { get; set; }
    public int DepartmanId { get; set; }

    public Departman Departman { get; set; }
}
class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }

    public List<Calisan> Calisanlar { get; set; }
}



#endregion

#region Navigation Property Nedir?
//İlişkisel tablolar arasındaki fiziksel erişimi entity class'ları üzerinden sağlayan property'lerdir.
//Bir property'nin navigation property olması için kesinlikle entity türünden olmalı.


//Navigation property'ler entity'lerdeki tanımlarına göre n'e n yahut 1'e n şeklinde ilişji türlerini ifade etmektedirler.
//Sonraki derslerde ilişkisel yapıları tam tefarruatlı pratikte incelerken bu özelliklerden yararlanacağız.
#endregion

#region İlişki Türleri
#region One to One
//Çalışan ile adresi arasındaki ilişki,
//Karı koca arasındaki ilişki.
#endregion

#region One to Many
//Çalışan ile Departman arasındaki ilişki,
//Anne ve çocukları arasındaki ilişki.
#endregion

#region Many to Many
//Çalışanlar ile projeler arasındaki ilişki,
//Kardeşler arasındaki ilişki. 
#endregion
#endregion

#region Entity Framework Core'da İlişki Yapılandırma Yöntemleri
#region Default Conventions
//Varsayılan entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemidir.

//Navigation property'leri kullanarak ilişki şablonlarını çıkarmaktadır.
#endregion

#region Data Annotations Attributes
//Entity'nin niteliklerine göre ince ayar yapmamızı sağlayan attribute'lardır. [Key] [ForeignKey]
#endregion

#region Fluent API
//Entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmamızı sağlayan yöntemlerdir.

#region HasOne
//İlgili Entity'nin ilişkisel entity'ye birebir ya da bire çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.

#endregion

#region HasMany
//İlgili Entity'nin ilişkisel entity'ye çoka bir ya da çoka çok olacak şekilde ilişkisini yapılandırmaya başlayan metottur.

#endregion

#region WithOne
//HasOne ya da HasMAny'den sonra bire bir ya da çoka bir olacak şekilde ilişki yapılandırmasını tamamlayan metottur.
#endregion

#region WithMany
//HasOne ya da HasMAny'den sonra bire çok ya da çoka çok olacak şekilde ilişki yapılandırmasını tamamlayan metottur.

#endregion
#endregion
#endregion