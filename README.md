# MyRecipeBook – Onion Architecture Örneği

## Genel Bakış
MyRecipeBook, Onion Architecture (Soğan Mimarisi) deseninin tarif yönetimi alanında uygulandığı, öğrenme odaklı bir backend projesidir. RESTful API üzerinden tarif CRUD işlemleri için minimal endpoint'ler sunar ve bunları Swagger ile dokümante eder. Çözüm container-friendly'dir ve Docker'da çalışan bir PostgreSQL veritabanı ile eşleştirilebilir.

```
MyRecipeBook
├── MyRecipeBook.Domain           # Saf domain entity'leri ve kontratlar
├── MyRecipeBook.Application      # DTO'lar, servis katmanı, AutoMapper profilleri
├── MyRecipeBook.Infrastructure   # EF Core DbContext, konfigürasyonlar, repository'ler
├── MyRecipeBook.WebApi           # ASP.NET Core giriş noktası (controller'lar, Program)
└── compose.yaml                  # Web API container'ı için Docker Compose tanımı
```

### Onion Architecture Katmanları
- **Domain** – Temel entity soyutlamalarını içerir. Harici bağımlılık yoktur.
- **Application** – Veri transfer nesneleri (DTO), servis arayüzleri/implementasyonları (`AppService`) ve AutoMapper profillerini barındırır. Sadece Domain katmanına bağımlıdır.
- **Infrastructure** – Entity Framework Core (PostgreSQL provider) kullanarak persistence'ı, somut repository'leri ve DbContext'i implemente eder. Hem Domain hem de Application soyutlamalarına bağımlıdır.
- **Presentation (WebApi)** – HTTP endpoint'lerini açığa çıkaran, dependency injection'ı yapılandıran ve test için Swagger UI'ı etkinleştiren ASP.NET Core minimal API katmanıdır.

## Kullanılan Teknolojiler
- .NET 8 / ASP.NET Core Web API
- Fluent API konfigürasyonları ile Entity Framework Core
- DTO ↔ entity mapping için AutoMapper
- Containerized çalıştırma için Docker & Docker Compose
- API keşfi için Swagger / Swashbuckle
- Birincil veri deposu olarak PostgreSQL (container üzerinden)

## Başlangıç

### Gereksinimler
- .NET SDK 8.0+
- Docker Desktop (veya uyumlu Docker engine)
- İsteğe bağlı: Migration'ları yerel olarak çalıştırmak için `dotnet-ef` global tool

### Docker Compose ile Çalıştırma
1. Docker Desktop'ın çalıştığından emin olun.
2. Repository root'undan (`MyRecipeBook-OnionArchitecture`), API container'ını build edip başlatın:
   ```powershell
   cd MyRecipeBook
   docker compose up --build
   ```
3. Web API container içinde `8080` portunda dinler. Host portuna map etmek isterseniz `compose.yaml` dosyasını güncelleyin (örneğin `ports: [ "8080:8080" ]` ekleyin).
4. API container'ına bir PostgreSQL instance'ına erişim sağlayın. Seçenekler:
   - Yerel bir Postgres Docker container'ı başlatın:
     ```powershell
     docker run --name recipe-db -e POSTGRES_PASSWORD=postgres -e POSTGRES_USER=postgres -e POSTGRES_DB=RecipeBookDb -p 5432:5432 -d postgres:15
     ```
   - Veya `compose.yaml` dosyasına bir `postgres` servisi ve kalıcılık için volume ekleyin.
5. Her iki container da çalıştıktan sonra, tarif endpoint'lerini keşfetmek ve test etmek için `http://localhost:8080/swagger` adresine gidin (map ettiğiniz porta göre ayarlayın).

### Yerel Olarak Çalıştırma (container olmadan)
1. PostgreSQL'i yerel olarak veya yukarıdaki gibi Docker üzerinden başlatın.
2. Gerekirse `MyRecipeBook.WebApi/appsettings.Development.json` dosyasındaki connection string'i güncelleyin.
3. Migration'ları uygulayın:
   ```powershell
   cd MyRecipeBook/MyRecipeBook.WebApi
   dotnet ef database update
   ```
4. Web API'yi çalıştırın:
   ```powershell
   dotnet run --project MyRecipeBook.WebApi
   ```
5. Swagger UI'a `https://localhost:5001/swagger` adresinden (development varsayılanı) veya konsolda belirtilen URL'den erişin.

## Veritabanı & Migration'lar
- İlk migration'lar `MyRecipeBook.Infrastructure/Migrations` altında bulunur ve `Recipes` tablosu için şemayı yakalar.
- Şemayı geliştirmek için `MyRecipeBook.WebApi` projesinden `dotnet ef migrations add <Name>` komutunu kullanın. Onion prensipleriyle tutarlılık için migration'ları Infrastructure projesi içinde tutun.

## API Yüzeyi
Mevcut endpoint'ler (hepsi `/api/recipes` altında):
- `GET /api/recipes` – Tarifleri listeler.
- `GET /api/recipes/{id}` – ID'ye göre tek bir tarif getirir.
- `POST /api/recipes` – Yeni bir tarif oluşturur.
- `PUT /api/recipes/{id}` – Mevcut bir tarifi günceller.
- `DELETE /api/recipes/{id}` – Bir tarifi siler.

Swagger UI, DTO'lardan türetilen request/response şemalarını gösterir. `POST /api/recipes` için örnek payload:

```json
{
  "name": "Klasik Pancake",
  "description": "Akçaağaç şurubu ile yumuşak pancake",
  "ingredients": ["Un", "Yumurta", "Süt"],
  "instructions": "Malzemeleri karıştırın ve orta ateşte pişirin"
}
```

## Geliştirme Notları
- Dependency injection, `Program.cs` içinde yapılandırılmıştır; `IRecipeAppService` ve `IRecipeRepository` gibi arayüzler somut implementasyonlarına bağlanır.
- AutoMapper profilleri, controller'ları ince tutmak için mapping tanımlarını merkezileştirir.
- Unit test'ler henüz dahil edilmemiştir; her katman için özel test projeleri eklemeyi düşünün (örneğin Application servisleri).
- Dockerfile varsayılan olarak Linux container'larını hedefler; Windows container'ları gerekiyorsa base image'leri ayarlayın.

## Yol Haritası Fikirleri
- Tarif yönetimi için authentication/authorization eklemek.
- Domain'i genişletmek (kategoriler, malzemeler, kullanıcı favorileri).
- Validasyon, sayfalama ve arama yetenekleri eklemek.
- Otomatik test'ler (unit/integration) ve CI pipeline sağlamak.
- Docker Compose'u seed'lenmiş PostgreSQL servisi ve volume yönetimi ile geliştirmek.

## Lisans
MIT Lisansı şartları altında dağıtılmaktadır. Detaylar için `LICENSE` dosyasına bakın.
