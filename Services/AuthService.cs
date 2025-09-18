using Microsoft.EntityFrameworkCore;
using Perpustakaan_Online.Models;
using System.Text.RegularExpressions;

namespace Perpustakaan_Online.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly List<string> _validDomains = new()
        {
            "gmail.com", "hotmail.com", "yahoo.com", "outlook.com"
        };

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, User? User)> RegisterAsync(string fullName, string email, string password)
        {
            // Validate email format and domain
            if (!ValidateEmail(email))
            {
                return (false, "Format email tidak valid.", null);
            }

            if (!IsValidDomain(email))
            {
                var validDomainsStr = string.Join(", ", _validDomains);
                return (false, $"Domain email tidak diizinkan. Gunakan domain: {validDomainsStr}", null);
            }

            // Validate password
            if (!ValidatePassword(password))
            {
                return (false, "Password harus minimal 8 karakter, mengandung huruf kapital, huruf kecil, dan angka. Tidak boleh mengandung karakter khusus.", null);
            }

            // Check if email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (existingUser != null)
            {
                return (false, "Email sudah terdaftar. Silakan gunakan email lain atau login.", null);
            }

            // Create new user
            var user = new User
            {
                FullName = fullName.Trim(),
                Email = email.ToLower().Trim(),
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return (true, "Registrasi berhasil! Silakan login.", user);
            }
            catch (Exception)
            {
                return (false, "Terjadi kesalahan saat registrasi. Silakan coba lagi.", null);
            }
        }

        public async Task<(bool Success, string Message, User? User)> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return (false, "Email dan password harus diisi.", null);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
            {
                return (false, "Email tidak ditemukan.", null);
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return (false, "Password salah.", null);
            }

            return (true, "Login berhasil!", user);
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Password must be at least 8 characters
            if (password.Length < 8)
                return false;

            // Must contain at least one uppercase letter
            if (!password.Any(char.IsUpper))
                return false;

            // Must contain at least one lowercase letter
            if (!password.Any(char.IsLower))
                return false;

            // Must contain at least one digit
            if (!password.Any(char.IsDigit))
                return false;

            // Must not contain special characters (symbols or punctuation)
            if (password.Any(c => !char.IsLetterOrDigit(c)))
                return false;

            return true;
        }

        public bool IsValidDomain(string email)
        {
            if (!ValidateEmail(email))
                return false;

            var domain = email.Split('@')[1].ToLower();
            return _validDomains.Contains(domain);
        }
    }
}