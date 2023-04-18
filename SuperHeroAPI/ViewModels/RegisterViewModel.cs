using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.ViewModels
{
    public class RegisterViewModel
    {
        /// <summary>
        /// Maximum 100 characters
        /// </summary>
        public string Name { get; set; }
        public string? Place { get; set; }
        /// <summary>
        /// Username can be unique varchar or email address
        /// </summary>
        [Required]
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Password is required
        /// </summary>
        [Required]
        public string Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}
