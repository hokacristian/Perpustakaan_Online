# Sistem Perpustakaan Online

Sistem manajemen perpustakaan digital yang dibangun dengan ASP.NET Core MVC 9 dan PostgreSQL (Supabase). Aplikasi ini menyediakan fitur lengkap untuk pengelolaan perpustakaan modern dengan antarmuka yang responsif dan user-friendly.

## 🆕 Update Terbaru (v2.0)

### ✅ Sistem Overdue yang Diperbaiki
- **Fixed**: User sekarang bisa mengembalikan buku meskipun sudah overdue
- **Fixed**: Count overdue di semua halaman sudah konsisten dan akurat
- **Fixed**: Filter overdue di admin dashboard sudah berfungsi dengan benar
- **Improved**: Sistem deteksi overdue yang lebih cerdas dan real-time

### ✅ Perbaikan Major
- **Enhanced**: Sample data dengan contoh transaksi overdue untuk testing
- **Improved**: Konsistensi status peminjaman di seluruh aplikasi
- **Fixed**: Bug pada perhitungan hari keterlambatan

## ✨ Fitur Utama

### 🔐 Sistem Autentikasi
- **Registrasi Pengguna** dengan validasi email ketat
- **Login/Logout** dengan session management
- **Validasi Email Domain**: Hanya menerima domain email tertentu (gmail.com, hotmail.com, yahoo.com, outlook.com, dll.)
- **Validasi Password**: Minimal 8 karakter, harus mengandung huruf kapital, huruf kecil, dan angka (tanpa karakter khusus)
- **Proteksi dari Email Duplikat**: Sistem menolak pendaftaran dengan email yang sudah terdaftar

### 📚 Manajemen Peminjaman Buku
- **Aturan Satu Buku per Pengguna**: Setiap pengguna hanya boleh meminjam satu buku dalam satu waktu
- **Tracking Peminjaman Real-time**: Monitoring status peminjaman secara langsung
- **Sistem Keterlambatan Cerdas**:
  - Deteksi otomatis buku yang terlambat dikembalikan
  - Perhitungan hari keterlambatan yang akurat
  - **User tetap bisa mengembalikan buku meskipun sudah terlambat**
  - Status overdue yang konsisten di seluruh sistem
- **Riwayat Peminjaman**: Pencatatan lengkap semua aktivitas peminjaman pengguna dengan status real-time
- **Kategori Buku**: Sistem kategorisasi untuk kemudahan pencarian dan pengelolaan

### 👨‍💼 Dashboard Admin
- **Monitoring Komprehensif**: Overview lengkap aktivitas perpustakaan
- **Manajemen Buku**: CRUD operations untuk katalog buku dengan validasi ketat
- **Manajemen Kategori**: CRUD operations untuk kategori buku dengan tracking jumlah buku per kategori
- **Tracking Peminjaman**: Monitoring semua transaksi peminjaman dengan filter status
- **Deteksi Keterlambatan**: Identifikasi otomatis buku yang terlambat dengan perhitungan hari keterlambatan real-time
- **Manajemen Pengguna**: Overview data pengguna sistem

### 🎨 User Interface Modern
- **Responsive Design**: Bootstrap 5 dengan dukungan mobile-first
- **Bootstrap Icons**: Ikon modern untuk pengalaman visual yang lebih baik
- **SweetAlert2 Notifications**: Popup notifications yang elegan untuk feedback pengguna
- **Password Visibility Toggle**: Fitur show/hide password pada form login dan register
- **Intuitive Navigation**: Menu navigasi yang mudah dipahami berdasarkan role pengguna
- **Real-time Status Updates**: Visual indicators untuk status peminjaman dan keterlambatan

## 🛠️ Teknologi yang Digunakan

- **Framework**: ASP.NET Core MVC 9
- **Database**: PostgreSQL (Supabase) dengan Entity Framework Core
- **ORM**: Entity Framework Core dengan Npgsql provider
- **UI Framework**: Bootstrap 5 + Bootstrap Icons + SweetAlert2
- **Authentication**: Custom authentication dengan BCrypt password hashing
- **Session Management**: ASP.NET Core Session
- **Security**: Input validation, Anti-forgery tokens, SQL injection prevention, XSS protection
- **Hosting**: Supabase untuk PostgreSQL database

## 📋 Persyaratan Sistem

- .NET 9 SDK
- Akun Supabase (gratis) untuk PostgreSQL database
- Visual Studio 2022 atau VS Code
- Git

## 🚀 Instalasi dan Setup

### 1. Clone Repository
```bash
git clone <repository-url>
cd Perpustakaan_Online
```

### 2. Setup Supabase Database

#### 2.1 Buat Akun Supabase
1. Kunjungi [supabase.com](https://supabase.com)
2. Daftar menggunakan GitHub atau email
3. Klik "New Project"
4. Pilih organization dan beri nama project (contoh: "perpustakaan-online")
5. Atur password database yang kuat
6. Pilih region terdekat (Southeast Asia - Singapore)
7. Klik "Create new project"

#### 2.2 Dapatkan Connection String
1. Setelah project selesai dibuat, buka **Settings** > **Database**
2. Scroll ke bagian **Connection String**
3. Pilih **.NET** dan copy connection string yang terlihat seperti:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=postgres.qkvunnpyqokkvxhmfwiu;Password=[YOUR-PASSWORD];Server=aws-1-ap-southeast-1.pooler.supabase.com;Port=5432;Database=postgres"
  }
}
```

### 3. Install Dependencies
```bash
dotnet restore
```

### 4. Konfigurasi Database
Edit file `appsettings.json` dan masukkan connection string Supabase:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=postgres.qkvunnpyqokkvxhmfwiu;Password=[YOUR-PASSWORD];Server=aws-1-ap-southeast-1.pooler.supabase.com;Port=5432;Database=postgres"
  }
}
```

**⚠️ Penting**:
- Ganti `[YOUR-PASSWORD]` dengan password database Supabase Anda
- Ganti `db.abcdefghijklmnopqrst` dengan host database Supabase Anda
- Pastikan menambahkan `Ssl Mode=Require;` di akhir connection string

### 5. Jalankan Aplikasi
```bash
dotnet run
```

Aplikasi akan berjalan di `https://localhost:5138` atau `http://localhost:5275`

### 6. Akses Aplikasi
- **URL**: Buka browser dan akses localhost sesuai port yang muncul di terminal
- **Auto-Setup**: Aplikasi akan otomatis membuat tabel, kategori, buku sample, dan akun demo
- **Login Admin**: Email: `admin@gmail.com`, Password: `Admin123`
- **Login User Sample**: Email: `budi.santoso@gmail.com`, Password: `User123`

## 📋 Apa yang Akan Anda Dapatkan Setelah Setup

Setelah menjalankan aplikasi untuk pertama kali, sistem akan otomatis membuat:

### 🗂️ Data Sample Lengkap
- **7 Kategori** buku (Novel, Biografi, Sejarah, Pendidikan, Filsafat, Puisi, Drama)
- **12 Buku Indonesia Terkenal** dengan kategori yang sesuai
- **2 Akun User** (Admin dan User sample)
- **2 Transaksi Peminjaman Sample**:
  - 1 transaksi **overdue** (Laskar Pelangi - terlambat 10 hari)
  - 1 transaksi **aktif** (Bumi Manusia - masih 9 hari lagi)

### 🚀 Fitur yang Langsung Bisa Digunakan
- **Admin Dashboard** dengan statistik real-time dan data sample overdue
- **User Dashboard** dengan status peminjaman yang menampilkan overdue warning
- **Katalog Buku** dengan pencarian dan filter kategori
- **Sistem Peminjaman** yang siap pakai dengan validasi lengkap
- **Sistem Overdue** yang sudah terintegrasi dan konsisten
- **Pengembalian Buku** yang bisa dilakukan meskipun sudah terlambat

## 👤 Akun Demo

Aplikasi menyediakan data sample yang sudah termasuk:

### Admin
- **Email**: admin@gmail.com
- **Password**: Admin123

### User Sample
- **Email**: budi.santoso@gmail.com
- **Password**: User123
- **Catatan**: User ini sudah memiliki sample data peminjaman (1 overdue, 1 aktif)

### Membuat User Baru
Anda juga dapat mendaftar sebagai pengguna baru melalui halaman registrasi dengan ketentuan:
- Email harus menggunakan domain yang diizinkan (gmail.com, hotmail.com, yahoo.com, outlook.com)
- Password minimal 8 karakter dengan kombinasi huruf kapital, huruf kecil, dan angka

## 📖 Cara Penggunaan

### Untuk Pengguna
1. **Registrasi**: Daftar dengan email yang valid dan password sesuai ketentuan
2. **Login**: Masuk dengan kredensial yang telah dibuat
3. **Jelajahi Buku**: Gunakan fitur pencarian dan filter kategori
4. **Pinjam Buku**: Klik tombol "Pinjam Buku" pada buku yang tersedia
5. **Kembalikan Buku**: Gunakan dashboard untuk mengembalikan buku
6. **Lihat Riwayat**: Pantau semua aktivitas peminjaman

### Untuk Admin
1. **Login Admin**: Gunakan akun admin untuk mengakses panel admin
2. **Dashboard**: Monitor aktivitas perpustakaan secara real-time
3. **Kelola Buku**: Tambah, edit, atau hapus buku dari katalog
4. **Monitor Peminjaman**: Lihat semua transaksi dan status keterlambatan
5. **Kelola Pengguna**: Lihat daftar pengguna yang terdaftar

## 🔒 Fitur Keamanan

- **Password Hashing**: BCrypt untuk enkripsi password
- **Input Validation**: Validasi di client-side dan server-side
- **SQL Injection Prevention**: Parameterized queries dengan Entity Framework
- **XSS Protection**: HTML encoding otomatis dalam Razor views
- **Session Security**: HttpOnly cookies dan session timeout
- **Anti-forgery Tokens**: Proteksi CSRF pada form submissions

## 📊 Struktur Database

### Tabel Users
- Id, FullName, Email, Password (BCrypt hashed), Role, CreatedAt, UpdatedAt

### Tabel Categories
- Id, Name, Description, CreatedAt, UpdatedAt

### Tabel Books
- Id, Title, Author, CategoryId, TotalCopies, AvailableCopies, Description, CreatedAt, UpdatedAt

### Tabel BorrowingTransactions
- Id, UserId, BookId, BorrowDate, DueDate, ReturnDate, Status, Notes, CreatedAt, UpdatedAt

**Computed Properties**:
- `BorrowingTransaction.IsOverdue`: Boolean property untuk cek status keterlambatan
- `BorrowingTransaction.DaysOverdue`: Integer property untuk menghitung hari keterlambatan

## 🎯 Business Rules

1. **Peminjaman**: Pengguna hanya boleh meminjam 1 buku dalam 1 waktu
2. **Periode Peminjaman**: Maksimal 14 hari dari tanggal peminjaman
3. **Keterlambatan**: Sistem otomatis menandai sebagai overdue setelah melewati due date
4. **Email Validation**: Hanya domain email tertentu yang diizinkan
5. **Password Policy**: Minimal 8 karakter dengan kombinasi huruf dan angka

## 🔧 Konfigurasi Lanjutan

### Environment Variables
Anda dapat menggunakan environment variables untuk konfigurasi:
- `ConnectionStrings__DefaultConnection`: Database connection string
- `ASPNETCORE_ENVIRONMENT`: Development/Production

### Logging
Aplikasi menggunakan built-in logging ASP.NET Core. Log dapat dilihat di console atau dikonfigurasi ke file.

## 🤝 Kontribusi

1. Fork repository
2. Buat feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit perubahan (`git commit -m 'Add some AmazingFeature'`)
4. Push ke branch (`git push origin feature/AmazingFeature`)
5. Buat Pull Request

## 📝 Troubleshooting

### Database Connection Error
- Pastikan connection string Supabase sudah benar
- Pastikan password database Supabase sudah benar
- Pastikan menambahkan `Ssl Mode=Require;` di connection string
- Cek apakah project Supabase sudah selesai provisioning (tunggu beberapa menit setelah pembuatan)

### Supabase Database Issues
- Jika mendapat error SSL/TLS, pastikan `Ssl Mode=Require;` ada di connection string
- Jika timeout, coba ganti region database ke yang lebih dekat
- Pastikan project Supabase tidak dalam status paused

### Migration Error (Jika Diperlukan)
```bash
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Port Already in Use
Edit `Properties/launchSettings.json` dan ubah port aplikasi

### Sample Data Tidak Muncul
- Hapus semua tabel di Supabase Dashboard > SQL Editor
- Restart aplikasi untuk trigger auto-seeding kembali

### Masalah Count Overdue
Jika count overdue menunjukkan 0 padahal ada buku terlambat:
- Pastikan aplikasi versi terbaru (setelah fix overdue system)
- Restart aplikasi untuk trigger auto-update status overdue
- Cek Supabase dashboard apakah ada data dengan status "Overdue"

### User Tidak Bisa Mengembalikan Buku Overdue
Masalah ini sudah diperbaiki di versi terbaru:
- Update code ke versi terbaru
- User sekarang bisa mengembalikan buku meskipun status sudah "Overdue"

## 📜 Lisensi

Projek ini dibuat untuk tujuan pembelajaran dan pengembangan sistem perpustakaan digital.

## 👨‍💻 Developer

Dikembangkan dengan ❤️ menggunakan ASP.NET Core MVC dan PostgreSQL

---

**Catatan**: Pastikan semua persyaratan sistem telah terpenuhi sebelum menjalankan aplikasi. Untuk bantuan lebih lanjut, silakan buka issue di repository ini.