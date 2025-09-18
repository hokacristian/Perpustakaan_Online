using Microsoft.EntityFrameworkCore;
using Perpustakaan_Online.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication Service
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    // Check if admin user exists
    if (!context.Users.Any(u => u.Email == "admin@gmail.com"))
    {
        var adminUser = new Perpustakaan_Online.Models.User
        {
            FullName = "Administrator Perpustakaan",
            Email = "admin@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
            Role = "Admin",
            CreatedAt = DateTime.UtcNow
        };
        context.Users.Add(adminUser);

        // Sample user account
        var sampleUser = new Perpustakaan_Online.Models.User
        {
            FullName = "Budi Santoso",
            Email = "budi.santoso@gmail.com",
            Password = BCrypt.Net.BCrypt.HashPassword("User123"),
            Role = "User",
            CreatedAt = DateTime.UtcNow
        };
        context.Users.Add(sampleUser);
        context.SaveChanges();
    }

    // Check if categories exist
    if (!context.Categories.Any())
    {
        var categories = new List<Perpustakaan_Online.Models.Category>
        {
            new() { Name = "Novel", Description = "Karya sastra berbentuk prosa naratif", CreatedAt = DateTime.UtcNow },
            new() { Name = "Biografi", Description = "Kisah hidup tokoh terkenal", CreatedAt = DateTime.UtcNow },
            new() { Name = "Sejarah", Description = "Buku tentang peristiwa masa lalu", CreatedAt = DateTime.UtcNow },
            new() { Name = "Pendidikan", Description = "Buku pembelajaran dan pengembangan diri", CreatedAt = DateTime.UtcNow },
            new() { Name = "Filsafat", Description = "Pemikiran dan kajian filosofis", CreatedAt = DateTime.UtcNow },
            new() { Name = "Puisi", Description = "Kumpulan karya puisi", CreatedAt = DateTime.UtcNow },
            new() { Name = "Drama", Description = "Naskah dan karya drama", CreatedAt = DateTime.UtcNow }
        };

        context.Categories.AddRange(categories);
        context.SaveChanges();
    }

    // Check if Indonesian famous books exist
    if (!context.Books.Any())
    {
        var novelCategory = context.Categories.First(c => c.Name == "Novel");
        var biografiCategory = context.Categories.First(c => c.Name == "Biografi");
        var sejarahCategory = context.Categories.First(c => c.Name == "Sejarah");
        var pendidikanCategory = context.Categories.First(c => c.Name == "Pendidikan");
        var filsafatCategory = context.Categories.First(c => c.Name == "Filsafat");
        var puisiCategory = context.Categories.First(c => c.Name == "Puisi");
        var dramaCategory = context.Categories.First(c => c.Name == "Drama");

        var indonesianBooks = new List<Perpustakaan_Online.Models.Book>
        {
            new()
            {
                Title = "Laskar Pelangi",
                Author = "Andrea Hirata",
                CategoryId = novelCategory.Id,
                TotalCopies = 8,
                AvailableCopies = 8,
                Description = "Novel tentang perjuangan anak-anak Belitung untuk mendapatkan pendidikan",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Bumi Manusia",
                Author = "Pramoedya Ananta Toer",
                CategoryId = novelCategory.Id,
                TotalCopies = 6,
                AvailableCopies = 6,
                Description = "Novel pertama dari Tetralogi Buru yang menceritakan kehidupan di masa kolonial",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Ronggeng Dukuh Paruk",
                Author = "Ahmad Tohari",
                CategoryId = novelCategory.Id,
                TotalCopies = 5,
                AvailableCopies = 5,
                Description = "Novel tentang kehidupan seorang penari ronggeng di desa Jawa",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Ayat-Ayat Cinta",
                Author = "Habiburrahman El Shirazy",
                CategoryId = novelCategory.Id,
                TotalCopies = 7,
                AvailableCopies = 7,
                Description = "Novel romantis berlatar belakang kehidupan mahasiswa Indonesia di Mesir",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Negeri 5 Menara",
                Author = "Ahmad Fuadi",
                CategoryId = novelCategory.Id,
                TotalCopies = 6,
                AvailableCopies = 6,
                Description = "Novel tentang kehidupan santri di pesantren dan impian mereka",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Soekarno: Biografi Singkat",
                Author = "Lambert Giebels",
                CategoryId = biografiCategory.Id,
                TotalCopies = 4,
                AvailableCopies = 4,
                Description = "Biografi lengkap Presiden pertama Indonesia",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Sejarah Indonesia Modern",
                Author = "M.C. Ricklefs",
                CategoryId = sejarahCategory.Id,
                TotalCopies = 5,
                AvailableCopies = 5,
                Description = "Kajian komprehensif sejarah Indonesia dari masa kolonial hingga modern",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Filosofi Teras",
                Author = "Henry Manampiring",
                CategoryId = filsafatCategory.Id,
                TotalCopies = 8,
                AvailableCopies = 8,
                Description = "Penerapan filosofi Stoikisme dalam kehidupan sehari-hari",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Chairil Anwar: Biografi Sastrawan",
                Author = "H.B. Jassin",
                CategoryId = biografiCategory.Id,
                TotalCopies = 3,
                AvailableCopies = 3,
                Description = "Biografi penyair besar Indonesia Chairil Anwar",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Hujan Bulan Juni",
                Author = "Sapardi Djoko Damono",
                CategoryId = puisiCategory.Id,
                TotalCopies = 4,
                AvailableCopies = 4,
                Description = "Kumpulan puisi dari maestro sastra Indonesia",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Tenggelamnya Kapal Van Der Wijck",
                Author = "Hamka",
                CategoryId = novelCategory.Id,
                TotalCopies = 5,
                AvailableCopies = 5,
                Description = "Novel klasik Indonesia tentang cinta dan adat Minangkabau",
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Title = "Cantik Itu Luka",
                Author = "Eka Kurniawan",
                CategoryId = novelCategory.Id,
                TotalCopies = 6,
                AvailableCopies = 6,
                Description = "Novel kontemporer tentang sejarah kelam Indonesia melalui mata perempuan",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Books.AddRange(indonesianBooks);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
