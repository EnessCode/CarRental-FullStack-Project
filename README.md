# 🚗 CarRental - Kurumsal Araç Kiralama & Blog Platformu

**CarRental**, .NET 8 Web API ve Core MVC mimarisiyle geliştirilmiş, modern araç kiralama süreçlerini yöneten uçtan uca bir platformdur. Proje, kurumsal standartlara uygun **Onion Architecture** prensipleriyle inşa edilmiştir.

---

## 📸 Proje Galerisi

### ⚙️ Operasyon ve Yönetim Panelleri
Sistemdeki tüm araçları, rezervasyon süreçlerini ve istatistikleri anlık olarak takip edebileceğiniz modern paneller.

<p align="center">
  <img src="screenshots/admin-dashbaord.png" width="48%" alt="Admin Dashboard" />
  <img src="screenshots/moderator-dashboard.png" width="48%" alt="Moderator Dashboard" />
</p>
<p align="center">
  <em>(Sol: Admin Paneli, Sağ: Moderatör Paneli)</em>
</p>

---

### 👤 Kullanıcı İşlemleri ve Güvenlik
Güvenli giriş, kayıt ve profil yönetim ekranları.

<p align="center">
  <img src="screenshots/login.png" width="31%" alt="Giriş Yap" />
  <img src="screenshots/register.png" width="31%" alt="Kayıt Ol" />
  <img src="screenshots/moderator-profile.png" width="31%" alt="Profilim" />
</p>
<p align="center">
  <em>(Sırasıyla: Giriş Yap, Kayıt Ol, Profil Sayfası)</em>
</p>

---

### 🚗 Araç Listeleme ve Kiralama Deneyimi
Kullanıcıların araçları filtreleyebildiği, özelliklerini inceleyebildiği ve rezervasyon sürecini yönettiği akış.

<p align="center">
  <img src="screenshots/car.png" width="48%" alt="Araç Filtreleme" />
  <img src="screenshots/car2.png" width="48%" alt="Araç Detayları" />
</p>
<p align="center">
  <img src="screenshots/reservation.png" width="31%" alt="Rezervasyon Yap" />
  <img src="screenshots/rentacarprocess.png" width="31%" alt="Kiralama Adımları" />
  <img src="screenshots/car3.png" width="31%" alt="Araç Özellikleri Detay" />
</p>
<p align="center">
  <em>(Sırasıyla: Filtreleme Ekranı, Araç Kartları, Rezervasyon Yap, Süreç Adımları, Detay Sayfası)</em>
</p>

---

### 📊 Süreç ve Destek Sayfaları
Kurumsal iletişim, hizmetler ve hakkımızda bölümleri ile admin tarafındaki kiralama süreci takibi.

<p align="center">
  <img src="screenshots/service.png" width="24%" alt="Hizmetlerimiz" />
  <img src="screenshots/about.png" width="24%" alt="Hakkımızda" />
  <img src="screenshots/contact.png" width="24%" alt="İletişim" />
  <img src="screenshots/admin-rentacarprocess.png" width="24%" alt="Admin Süreç Takibi" />
</p>

---

### 📝 Blog ve Etkileşim
Zengin blog içerikleri, yazar detayları ve kullanıcı yorumları.

<p align="center">
  <img src="screenshots/blog.png" width="48%" alt="Blog Listesi" />
  <img src="screenshots/blogdetail1.png" width="48%" alt="Blog Detayı" />
</p>
<p align="center">
  <img src="screenshots/blogdetail2.png" width="48%" alt="Yorumlar Part 1" />
  <img src="screenshots/blogdetail3.png" width="48%" alt="Yorumlar Part 2" />
</p>
<p align="center">
  <em>(Sırasıyla: Blog Index, Blog Detay, Yorumlar ve Yorum Yapma Bölümleri)</em>
</p>

---

## ✨ Öne Çıkan Özellikler

### 🔐 Gelişmiş Yetkilendirme (RBAC)
- **Dinamik Rol Yönetimi:** Kullanıcıları Admin paneli üzerinden görüntüleme ve tek tıkla Moderatör yetkisi atama.
- **Güvenli Giriş:** BCrypt şifreleme ve JWT (JSON Web Token) tabanlı oturum yönetimi.

### 🚗 Rezervasyon & Operasyon
- **Kiralama Akışı:** Lokasyon, vites, yakıt gibi özelliklere göre araç arama ve rezervasyon süreci yönetimi.
- **Email Confirmation:** Rezervasyon tamamlandığında kullanıcıya gönderilen otomatik HTML onay e-postası.

### 🏗️ Mimari ve Teknik Altyapı
- **Onion Architecture:** Bağımlılıkları minimize eden, test edilebilir çok katmanlı yapı.
- **CQRS & Mediator:** MediatR ile ayrıştırılmış komut ve sorgu yönetimi.
- **Eager Loading:** EF Core ile ilişkili tabloların (`Include`) performanslı yönetimi.

---

## 🛠️ Teknolojiler

- **Backend:** C#, .NET 8, Web API
- **Design Patterns:** CQRS, Mediator, Repository, Unit of Work
- **Database:** MSSQL, Entity Framework Core (Code First)
- **Frontend:** ASP.NET Core MVC, Bootstrap 5, Remix Icons
- **Security:** JWT, User Secrets, BCrypt

---

## ⚙️ Kurulum ve Güvenlik

Proje güvenliği için hassas veriler `appsettings.json` yerine **Secret Manager**'da tutulmaktadır. Kurulum için:

1. **Repoyu klonlayın.**
2. **WebApi** projesine sağ tıklayıp **Manage User Secrets** diyerek aşağıdaki şablonu doldurun:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CarRentalDb;Integrated Security=true;TrustServerCertificate=true;"
  },
  "Jwt": {
    "Key": "Minimum32KarakterlikGucluBirSecretKey",
    "Issuer": "CarRentalIdentity",
    "Audience": "CarRentalClients"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password"
  }
}

3. Migration Uygulayın:
   Package Manager Console -> Update-Database

Geliştirici: EnessCode