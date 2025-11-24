## Proje Açıklaması

Bu proje, **.NET 8** kullanarak baştan sona kendim geliştirmeyi hedeflediğim bir **Yemek Tarifi Uygulaması**. Amacım sadece bir uygulama ortaya çıkarmak değil; aynı zamanda **Onion Architecture, katmanlı mimari, EF Core, PostgreSQL, Docker** gibi konuları gerçekten derinlemesine öğrenmek. Daha önce **abp.io** gibi güçlü altyapılarla çalıştığım için birçok şey hazır geliyordu, bu projede ise her adımı kendim tasarlayıp uygulamayı özellikle tercih ettim.

Veritabanı olarak **PostgreSQL** kullandım ve tamamen **Docker** üzerinden ayağa kalkacak şekilde yapılandırdım. Böylece projeyi klonlayan biri, bilgisayarına ekstra bir şey kurmaya gerek kalmadan Docker ile veritabanını hazır hale getirebiliyor. Uygulamanın veri erişim kısmında **EF Core** tercih ettim; hem migration yönetimi hem de repository yapısını daha iyi oturtmak için güzel bir pratik oldu.

Mimari tarafta **Onion Architecture** yapısını kullandım. **Domain, Application, Infrastructure ve API** katmanları arasında bağımlılıkların nasıl doğru kurulacağını öğrenmek ve sürdürülebilir bir yapı oluşturmak benim için önemliydi. Bu yapı sayesinde proje hem genişlemeye açık hem de okunabilir bir formda ilerliyor.

Bu dokümanda, projeyi indirip kendi ortamında çalıştırmak isteyen herkes için kurulum adımlarını, mimariyi ve kullanılan teknolojilerin neden tercih edildiğini mümkün olduğunca anlaşılır bir şekilde anlatmaya çalıştım. Umuyorum ki hem proje geliştiriciler hem de bu mimariyi öğrenmek isteyenler için faydalı bir kaynak olur.

## Onion Architecture Nedir ve Bu Projede Neden Kullandım?

Bu projede özellikle Onion Architecture kullanmayı tercih ettim çünkü hem temiz bir kod yapısı sağlıyor hem de uzun vadede projeyi büyütmeyi kolaylaştırıyor.
**Onion Architecture’ın temel fikri şu:**
İş kurallarının yer aldığı merkez (Domain) hiçbir şeye bağımlı olmaz; diğer tüm katmanlar merkeze doğru bağımlıdır ama merkez dışarıya bağımlı olmaz.

<details> 
   <summary><strong>1️⃣ En iç katman: Domain</strong></summary>
   İş kurallarının ve temel iş mantığının tanımlandığı katmandır.
   <ul>
     <li>Entity</li>
     <li>Value Object</li>
     <li>Domain servisleri (Karmaşık iş kuralları için. Ör: Puan hesaplama)</li>
     <li>Repository arayüzleri</li>
   </ul>
</details>

<details> 
   <summary><strong>2️⃣ Application Katmanı</strong></summary>
   Kullanıcı senaryolarını ve uygulama iş akışını yönetir.
   <ul>
     <li>AppService (CRUD işlemler)</li>
     <li>DTO</li>
     <li>Automapper: Domain nesneleri ve DTO'lar arasında dönüşüm sağlar.</li>
     <li>İzinler ve yetkinlendirme</li>
   </ul>
</details>

<details> 
   <summary><strong>3️⃣ Infrastructure Katmanı</strong></summary>
   Dış servislerle ve veritabanı ile iletişimi sağlar.
   <ul>
     <li>Db bağlantısı ve migrationlar (DbContext sınıfında bağlantı)</li>
     <li>Repository implementasyonları</li>
     <li>Entity Framework Core</li>
   </ul>
</details>

<details> 
   <summary><strong>4️⃣ API Katmanı</strong></summary>
   Kullanıcı ile etkileşimi sağlar.
   <ul>
     <li>Sunum katmanı</li>
     <li>Controller</li>
   </ul>
</details>

## JWT Authentication (Token Bazlı Yetkilendirme)
Projede kullanıcı doğrulama ve yetkilendirme işlemleri için JWT (JSON Web Token) kullandım.
Amacım, kullanıcı giriş yaptıktan sonra uygulama içinde güvenli bir şekilde yetkilerini kontrol edebilmek ve her isteğin doğruluğunu garanti altına almak.

### JWT => Header . Payload . Signature
<ul>
   <li><strong>Header: </strong>JWT'nin tipini ve encoding formatını tanımlar.</li>
   <li><strong>Payload: </strong>Yetkilendirme bilgilerini içeren verilerdir ve genellikle JSON formatında tutulur.</li>
   <li><strong>Signature: </strong>Signature, header ve payload verilerinin hash değerini içerir ve JWT'nin doğruluğunu kontrol etmek için kullanılır.</li>
</ul>

### Temel Akış
 <ol>
   <li>Kullanıcı register veya login olur.</li>
   <li>Sistem kullanıcı bilgilerini doğrular.</li>
   <li>Eğer bilgiler doğruysa, kullanıcıya JWT token üretilir ve geri gönderilir.</li>
   <li>Kullanıcı bu token’ı sonraki isteklerinde HTTP Header üzerinden gönderir.</li>
   <li>API, gelen token’ı doğrular ve kullanıcının yetkilerini kontrol eder.</li>
 </ol>
 Bu sayede:
 <ul>
   <li>Her istek için kullanıcı kimliği doğrulanmış olur</li>
   <li>Rollere veya yetkilere göre erişim kontrolü sağlanabilir</li>
   <li>Token süresi dolduğunda yeniden login gereklidir, böylece güvenlik artırılır</li>
 </ul>

### Kullanılan Teknolojiler ve Yapı
<ul>
   <li><strong>Microsoft.AspNetCore.Authentication.JwtBearer</strong> ile JWT doğrulaması</li>
   <li><strong>JwtSettings</strong> sınıfı ile token ayarlarını yapılandırma (Key, Issuer, Audience, ExpireMinutes)</li>
   <li><strong>Bcrypt</strong> ile şifreleri güvenli şekilde hashleme ve doğrulama</li>
   <li><strong>Hashing:</strong> Kullanıcı şifreleri veritabanına hash’lenmiş şekilde kaydedilir, bu sayede güvenlik sağlanır.</li>
   <li><strong>Verify:</strong> Login sırasında girilen şifre hash ile karşılaştırılır.</li>
 </ul>

 ### API Katmanında Kullanımı
 <ul>
  <li>[Authorize] attribute’u ile controller veya action bazında yetkilendirme uygulanabilir.</li>
  <li>Token geçerli değilse, kullanıcıya 401 Unauthorized döner.</li>
  <li>Token geçerli ve yetkili ise, istek ilgili servisler üzerinden işlenir.</li>
</ul>

 ### Faydaları
 <ul>
  <li>Stateless authentication (sunucu tarafında oturum tutmaya gerek yok)</li>
  <li>Kolay rol yönetimi ve yetkilendirme</li>
  <li>Verilerin kolayca okunabilri ve kodlanabilir olması</li>
  <li>Uygulamalar arasında güvenli veri akışı sağlaması</li>
</ul>

