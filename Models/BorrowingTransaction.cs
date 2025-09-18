using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan_Online.Models
{
    public class BorrowingTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Borrowed"; // Borrowed, Returned, Overdue

        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Book Book { get; set; } = null!;

        // Computed property for overdue status
        [NotMapped]
        public bool IsOverdue => Status == "Borrowed" && DateTime.UtcNow > DueDate;

        [NotMapped]
        public int DaysOverdue
        {
            get
            {
                if (!IsOverdue) return 0;
                var today = DateTime.UtcNow.Date;
                var dueDate = DueDate.Date;
                return (today - dueDate).Days;
            }
        }
    }
}