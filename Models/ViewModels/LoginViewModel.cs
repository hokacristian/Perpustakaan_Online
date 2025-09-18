using System.ComponentModel.DataAnnotations;

namespace Perpustakaan_Online.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email harus diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password harus diisi")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Ingat saya")]
        public bool RememberMe { get; set; }
    }
}