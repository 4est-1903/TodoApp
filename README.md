# 📝 TodoApp

ASP.NET Core 8 ile geliştirilmiş, **REST API + MVC Web** mimarisinden oluşan görev yönetim uygulaması. Backend ve frontend birbirinden ayrı projeler olarak tasarlanmış olup frontend, API ile HTTP üzerinden haberleşir.

---

## 🏗️ Proje Yapısı

```
TodoApp/
├── TodoApp.API/        # REST API katmanı (Web API)
└── TodoApp.Web/        # MVC Web arayüzü katmanı
```

### TodoApp.API
- Görevlerin CRUD işlemlerini yöneten RESTful API
- MySQL veritabanı bağlantısı (Entity Framework Core + Pomelo)
- FluentValidation ile giriş doğrulaması
- AutoMapper ile DTO dönüşümleri
- Merkezi hata yönetimi için özel Middleware
- Swagger / OpenAPI dokümantasyonu

### TodoApp.Web
- API'yi tüketen ASP.NET Core MVC uygulaması
- Bootstrap 5 tabanlı, responsive kullanıcı arayüzü
- İstatistik kartları (toplam, tamamlanan, devam eden görev sayıları)
- Arama ve filtreleme desteği

---

## 🚀 Özellikler

- ✅ Görev oluşturma (başlık + açıklama)
- ✅ Görevleri listeleme
- ✅ Göreve göre arama
- ✅ Tamamlanmış / tamamlanmamış görevleri filtreleme
- ✅ Görev tamamlama durumunu değiştirme (toggle)
- ✅ Görev silme
- ✅ Form doğrulaması (FluentValidation + DataAnnotations)
- ✅ Merkezi hata yönetimi

---

## 🛠️ Teknolojiler

| Katman | Teknoloji |
|--------|-----------|
| Framework | ASP.NET Core 8 |
| ORM | Entity Framework Core 8 |
| Veritabanı | MySQL (Pomelo.EntityFrameworkCore.MySql) |
| Doğrulama | FluentValidation 12 |
| Nesne Eşleme | AutoMapper 12 |
| API Dokümantasyonu | Swagger (Swashbuckle) |
| Frontend | Bootstrap 5, jQuery |

---

## ⚙️ Kurulum

### Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- MySQL Server (port 3306)

### 1. Repoyu Klonla

```bash
git clone https://github.com/kullanici-adi/TodoApp.git
cd TodoApp
```

### 2. Veritabanı Bağlantısını Yapılandır

`TodoApp.API/appsettings.json` dosyasındaki bağlantı dizesini kendi MySQL bilgilerinle güncelle:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=TodoDb;user=root;password=YOUR_PASSWORD;"
}
```

### 3. Veritabanını Oluştur (Migration)

```bash
cd TodoApp.API
dotnet ef database update
```

### 4. API'yi Başlat

```bash
cd TodoApp.API
dotnet run
```

API varsayılan olarak `https://localhost:7xxx` adresinde çalışır. Swagger arayüzüne `/swagger` yolundan erişebilirsin.

### 5. Web Uygulamasını Başlat

```bash
cd TodoApp.Web
dotnet run
```

---

## 📡 API Endpoint'leri

| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | `/api/Todo` | Tüm görevleri listele |
| GET | `/api/Todo?search=...` | Başlık/açıklamaya göre ara |
| GET | `/api/Todo?isCompleted=true` | Tamamlanma durumuna göre filtrele |
| GET | `/api/Todo/{id}` | Belirli bir görevi getir |
| POST | `/api/Todo` | Yeni görev oluştur |
| PUT | `/api/Todo/{id}` | Görevi güncelle |
| DELETE | `/api/Todo/{id}` | Görevi sil |

---

## 📁 Klasör Yapısı (API)

```
TodoApp.API/
├── Controllers/        # API controller'ları
├── Data/               # DbContext
├── DTOs/               # Veri transfer nesneleri ve validator'lar
├── Middlewares/        # Özel hata yakalama middleware'i
├── Migrations/         # EF Core migration dosyaları
└── Models/             # Veritabanı entity modelleri
```
