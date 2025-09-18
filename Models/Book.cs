using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan_Online.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Judul buku harus diisi")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Penulis harus diisi")]
        [StringLength(100)]
        public string Author { get; set; } = string.Empty;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Total eksemplar harus diisi")]
        [Range(1, int.MaxValue, ErrorMessage = "Total eksemplar minimal 1")]
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();
    }
}