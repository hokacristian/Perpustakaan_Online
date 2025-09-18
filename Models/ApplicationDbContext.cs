using Microsoft.EntityFrameworkCore;
using Perpustakaan_Online.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowingTransaction> BorrowingTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Role).HasDefaultValue("User");
        });

        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Configure Book entity
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasOne(d => d.Category)
                .WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure BorrowingTransaction entity
        modelBuilder.Entity<BorrowingTransaction>(entity =>
        {
            entity.HasOne(d => d.User)
                .WithMany(p => p.BorrowingTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Book)
                .WithMany(p => p.BorrowingTransactions)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.Status).HasDefaultValue("Borrowed");
        });

        // No seed data here - we'll use runtime seeding in Program.cs
    }
}