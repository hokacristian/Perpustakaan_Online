using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Perpustakaan_Online.Controllers
{
    public class UserController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [RequireAuth]
        public async Task<IActionResult> Dashboard()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var currentBorrowing = await _context.BorrowingTransactions
                .Include(bt => bt.Book)
                .Where(bt => bt.UserId == CurrentUserId && bt.Status == "Borrowed")
                .FirstOrDefaultAsync();

            ViewBag.CurrentBorrowing = currentBorrowing;
            ViewBag.UserName = CurrentUserName;

            return View();
        }

        [RequireAuth]
        public async Task<IActionResult> Books(string search = "", int? categoryId = null)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var booksQuery = _context.Books
                .Include(b => b.Category)
                .Where(b => b.AvailableCopies > 0);

            if (!string.IsNullOrEmpty(search))
            {
                booksQuery = booksQuery.Where(b =>
                    b.Title.Contains(search) ||
                    b.Author.Contains(search) ||
                    b.Category.Name.Contains(search));
            }

            if (categoryId.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.CategoryId == categoryId.Value);
            }

            var books = await booksQuery.OrderBy(b => b.Title).ToListAsync();
            var categories = await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Categories = categories;
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;

            return View(books);
        }

        [RequireAuth]
        public async Task<IActionResult> BorrowHistory()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var borrowHistory = await _context.BorrowingTransactions
                .Include(bt => bt.Book)
                    .ThenInclude(b => b.Category)
                .Where(bt => bt.UserId == CurrentUserId)
                .OrderByDescending(bt => bt.BorrowDate)
                .ToListAsync();

            return View(borrowHistory);
        }

        [RequireAuth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrowBook(int bookId)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            // Check if user already has a borrowed book
            var existingBorrowing = await _context.BorrowingTransactions
                .AnyAsync(bt => bt.UserId == CurrentUserId && bt.Status == "Borrowed");

            if (existingBorrowing)
            {
                TempData["ErrorMessage"] = "Anda masih memiliki buku yang belum dikembalikan. Silakan kembalikan buku tersebut terlebih dahulu.";
                return RedirectToAction("Books");
            }

            // Check if book is available
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.AvailableCopies <= 0)
            {
                TempData["ErrorMessage"] = "Buku tidak tersedia untuk dipinjam.";
                return RedirectToAction("Books");
            }

            // Create borrowing transaction
            if (!CurrentUserId.HasValue)
            {
                return RedirectToLogin();
            }

            var borrowingTransaction = new Models.BorrowingTransaction
            {
                UserId = CurrentUserId.Value,
                BookId = bookId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14), // 2 weeks borrowing period
                Status = "Borrowed"
            };

            // Update book availability
            book.AvailableCopies--;

            _context.BorrowingTransactions.Add(borrowingTransaction);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Buku '{book.Title}' berhasil dipinjam. Harap dikembalikan sebelum {borrowingTransaction.DueDate:dd/MM/yyyy}.";
            return RedirectToAction("Dashboard");
        }

        [RequireAuth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(int transactionId)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var transaction = await _context.BorrowingTransactions
                .Include(bt => bt.Book)
                .FirstOrDefaultAsync(bt => bt.Id == transactionId && bt.UserId == CurrentUserId && bt.Status == "Borrowed");

            if (transaction == null)
            {
                TempData["ErrorMessage"] = "Transaksi peminjaman tidak ditemukan.";
                return RedirectToAction("Dashboard");
            }

            // Update transaction
            transaction.ReturnDate = DateTime.UtcNow;
            transaction.Status = "Returned";
            transaction.UpdatedAt = DateTime.UtcNow;

            // Update book availability
            transaction.Book.AvailableCopies++;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Buku '{transaction.Book.Title}' berhasil dikembalikan.";
            return RedirectToAction("Dashboard");
        }
    }
}