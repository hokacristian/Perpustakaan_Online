# Sistem Perpustakaan Online

Sistem manajemen perpustakaan digital yang dibangun dengan ASP.NET Core MVC 9 dan PostgreSQL. Aplikasi ini menyediakan fitur lengkap untuk pengelolaan perpustakaan modern dengan antarmuka yang responsif dan user-friendly.

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
- **Notifikasi Keterlambatan**: Sistem otomatis mendeteksi dan menampilkan buku yang terlambat dikembalikan
- **Riwayat Peminjaman**: Pencatatan lengkap semua aktivitas peminjaman pengguna

### 👨‍💼 Dashboard Admin
- **Monitoring Komprehensif**: Overview lengkap aktivitas perpustakaan
- **Manajemen Buku**: CRUD operations untuk katalog buku
- **Tracking Peminjaman**: Monitoring semua transaksi peminjaman
- **Deteksi Keterlambatan**: Identifikasi otomatis buku yang terlambat dengan perhitungan hari keterlambatan
- **Manajemen Pengguna**: Overview data pengguna sistem

### 🎨 User Interface Modern
- **Responsive Design**: Bootstrap 5 dengan dukungan mobile-first
- **Bootstrap Icons**: Ikon modern untuk pengalaman visual yang lebih baik
- **Real-time Notifications**: Alert system untuk feedback pengguna
- **Intuitive Navigation**: Menu navigasi yang mudah dipahami berdasarkan role pengguna

## 🛠️ Teknologi yang Digunakan

- **Framework**: ASP.NET Core MVC 9
- **Database**: PostgreSQL dengan Entity Framework Core
- **ORM**: Entity Framework Core dengan Npgsql provider
- **UI Framework**: Bootstrap 5 + Bootstrap Icons
- **Authentication**: Custom authentication dengan BCrypt password hashing
- **Session Management**: ASP.NET Core Session
- **Security**: Input validation, SQL injection prevention, XSS protection

## 📋 Persyaratan Sistem

- .NET 9 SDK
- PostgreSQL Server
- Visual Studio 2022 atau VS Code
- Git

## 🚀 Instalasi dan Setup

### 1. Clone Repository
```bash
git clone <repository-url>
cd Perpustakaan_Online
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. Konfigurasi Database
Edit file `appsettings.json` dan sesuaikan connection string PostgreSQL:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=your_username;Password=your_password;Server=your_server;Port=5432;Database=your_database"
  }
}
```

### 4. Migration Database
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Jalankan Aplikasi
```bash
dotnet run
```

Aplikasi akan berjalan di `https://localhost:5001`

## 👤 Akun Demo

### Admin
- **Email**: admin@library.com
- **Password**: Admin123

### User
Daftar sebagai pengguna baru melalui halaman registrasi

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
- Id, FullName, Email, PasswordHash, Role, CreatedAt, UpdatedAt

### Tabel Books
- Id, Title, Author, ISBN, Category, TotalCopies, AvailableCopies, Description, CreatedAt, UpdatedAt

### Tabel BorrowingTransactions
- Id, UserId, BookId, BorrowDate, DueDate, ReturnDate, Status, Notes, CreatedAt, UpdatedAt

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
- Pastikan PostgreSQL server berjalan
- Periksa connection string di appsettings.json
- Pastikan database dan user memiliki permission yang sesuai

### Migration Error
```bash
dotnet ef database drop
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Port Already in Use
Edit `Properties/launchSettings.json` dan ubah port aplikasi

## 📜 Lisensi

Projek ini dibuat untuk tujuan pembelajaran dan pengembangan sistem perpustakaan digital.

## 👨‍💻 Developer

Dikembangkan dengan ❤️ menggunakan ASP.NET Core MVC dan PostgreSQL

---

**Catatan**: Pastikan semua persyaratan sistem telah terpenuhi sebelum menjalankan aplikasi. Untuk bantuan lebih lanjut, silakan buka issue di repository ini.