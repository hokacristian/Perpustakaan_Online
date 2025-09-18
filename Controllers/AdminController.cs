using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Perpustakaan_Online.Models;

namespace Perpustakaan_Online.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [RequireAdmin]
        public async Task<IActionResult> Dashboard()
        {
            var totalBooks = await _context.Books.CountAsync();
            var totalUsers = await _context.Users.Where(u => u.Role == "User").CountAsync();
            var activeBorrowings = await _context.BorrowingTransactions.Where(bt => bt.Status == "Borrowed").CountAsync();
            var overdueBooks = await _context.BorrowingTransactions
                .Where(bt => bt.Status == "Borrowed" && bt.DueDate < DateTime.UtcNow)
                .CountAsync();

            ViewBag.TotalBooks = totalBooks;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.ActiveBorrowings = activeBorrowings;
            ViewBag.OverdueBooks = overdueBooks;

            // Recent borrowings
            var recentBorrowings = await _context.BorrowingTransactions
                .Include(bt => bt.User)
                .Include(bt => bt.Book)
                .OrderByDescending(bt => bt.BorrowDate)
                .Take(10)
                .ToListAsync();

            return View(recentBorrowings);
        }

        [RequireAdmin]
        public async Task<IActionResult> Books()
        {
            var books = await _context.Books
                .Include(b => b.Category)
                .OrderBy(b => b.Title)
                .ToListAsync();
            return View(books);
        }

        [RequireAdmin]
        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(categories);
        }

        [RequireAdmin]
        public async Task<IActionResult> BorrowingTransactions(string status = "")
        {
            var transactionsQuery = _context.BorrowingTransactions
                .Include(bt => bt.User)
                .Include(bt => bt.Book)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                if (status == "Overdue")
                {
                    transactionsQuery = transactionsQuery.Where(bt => bt.Status == "Borrowed" && bt.DueDate < DateTime.UtcNow);
                }
                else
                {
                    transactionsQuery = transactionsQuery.Where(bt => bt.Status == status);
                }
            }

            var transactions = await transactionsQuery
                .OrderByDescending(bt => bt.BorrowDate)
                .ToListAsync();

            ViewBag.Status = status;
            return View(transactions);
        }

        [RequireAdmin]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users
                .Include(u => u.BorrowingTransactions)
                    .ThenInclude(bt => bt.Book)
                .Where(u => u.Role == "User")
                .OrderBy(u => u.FullName)
                .ToListAsync();

            return View(users);
        }

        [RequireAdmin]
        public async Task<IActionResult> AddBook()
        {
            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View();
        }

        [RequireAdmin]
        public IActionResult AddCategory()
        {
            return View();
        }

        [RequireAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(Book book)
        {
            if (ModelState.IsValid)
            {
                book.AvailableCopies = book.TotalCopies;
                book.CreatedAt = DateTime.UtcNow;

                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Buku berhasil ditambahkan.";
                return RedirectToAction("Books");
            }

            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(book);
        }

        [RequireAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.UtcNow;

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Kategori berhasil ditambahkan.";
                return RedirectToAction("Categories");
            }

            return View(category);
        }

        [RequireAdmin]
        public async Task<IActionResult> EditBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(book);
        }

        [RequireAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingBook = await _context.Books.FindAsync(id);
                    if (existingBook == null)
                    {
                        return NotFound();
                    }

                    existingBook.Title = book.Title;
                    existingBook.Author = book.Author;
                    existingBook.CategoryId = book.CategoryId;
                    existingBook.Description = book.Description;
                    existingBook.TotalCopies = book.TotalCopies;
                    existingBook.AvailableCopies = book.AvailableCopies;
                    existingBook.UpdatedAt = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Buku berhasil diperbarui.";
                    return RedirectToAction("Books");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Terjadi kesalahan saat memperbarui data.");
                }
            }

            return View(book);
        }

        [RequireAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                // Check if book has active borrowings
                var activeBorrowings = await _context.BorrowingTransactions
                    .AnyAsync(bt => bt.BookId == id && bt.Status == "Borrowed");

                if (activeBorrowings)
                {
                    TempData["ErrorMessage"] = "Tidak dapat menghapus buku yang sedang dipinjam.";
                }
                else
                {
                    _context.Books.Remove(book);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Buku berhasil dihapus.";
                }
            }

            return RedirectToAction("Books");
        }

        [RequireAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsOverdue(int transactionId)
        {
            var transaction = await _context.BorrowingTransactions.FindAsync(transactionId);
            if (transaction != null && transaction.Status == "Borrowed")
            {
                transaction.Status = "Overdue";
                transaction.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Status peminjaman berhasil diubah menjadi terlambat.";
            }

            return RedirectToAction("BorrowingTransactions");
        }

        [RequireAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCategory = await _context.Categories.FindAsync(category.Id);
                    if (existingCategory == null)
                    {
                        return NotFound();
                    }

                    existingCategory.Name = category.Name;
                    existingCategory.Description = category.Description;
                    existingCategory.UpdatedAt = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Kategori berhasil diperbarui.";
                    return RedirectToAction("Categories");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Terjadi kesalahan saat memperbarui data.");
                }
            }

            return RedirectToAction("Categories");
        }

        [RequireAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                if (category.Books.Any())
                {
                    TempData["ErrorMessage"] = "Tidak dapat menghapus kategori yang memiliki buku.";
                }
                else
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Kategori berhasil dihapus.";
                }
            }

            return RedirectToAction("Categories");
        }
    }
}