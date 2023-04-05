using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }
    }
}
