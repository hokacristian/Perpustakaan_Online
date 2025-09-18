using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Perpustakaan_Online.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Connection()
        {
            try
            {
                await _context.Database.CanConnectAsync();
                return Json(new { success = true, message = "Database connection successful!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Database connection failed: {ex.Message}" });
            }
        }
    }
}