using Perpustakaan_Online.Models;

namespace Perpustakaan_Online.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, User? User)> RegisterAsync(string fullName, string email, string password);
        Task<(bool Success, string Message, User? User)> LoginAsync(string email, string password);
        bool ValidateEmail(string email);
        bool ValidatePassword(string password);
        bool IsValidDomain(string email);
    }
}